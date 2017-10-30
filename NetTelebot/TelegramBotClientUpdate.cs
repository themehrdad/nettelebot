﻿using System;
using System.Linq;
using System.Threading;
using NetTelebot.Result;
using NetTelebot.Type;
using RestSharp;

namespace NetTelebot
{
    /* About tests
     * After adding the class field, you need to add the following tests:
     *  
     * NetTelebot.Tests.RequestToMockTest.[ClassName]
     * 
     * [ClassName] = TelegramBotClientTest if you want to test the method
     * [ClassName] = TelegramBotGetUpdatesTest if you are testing for updates
     * [ClassName] = TelegramBotInlineKeyboardTest or TelegramBotKeyboardTest if you test keyboard.
     * [ClassName] = TelegramBotEventHandlerTest if you test event handler
     * 
     * Also you can check how the written added methods work in the namespace classes NetTelebot.Tests.RequestToTelegramTest.
     * There are requests to the telegram servers
     */

    /* About this partial class
     * 
     * Part of the class, only methods for handling updates.
     * 
     */

    /// <summary>
    /// The main class to use Telegram Bot API. 
    /// Get an instance of this class and set the Token property and start calling methods.
    /// </summary>
    public partial class TelegramBotClient
    {
        private const string mGetUpdatesUri = "/bot{0}/getUpdates";

        private Timer mUpdateTimer;
        private int mLastUpdateId;

        /// <summary>
        /// Occurs when updates error.
        /// </summary>
        public event UnhandledExceptionEventHandler GetUpdatesError;

        /// <summary>
        /// Whenever a message is sent to your bot, this event will be raised.
        /// </summary>
        public event EventHandler<TelegramUpdateEventArgs> UpdatesReceived;

        /// <summary>
        /// Gets first 100 messages sent to your bot.
        /// </summary>
        /// <returns>Returns a class containing messages sent to your bot</returns>
        public GetUpdatesResult GetUpdates()
        {
            return GetUpdatesInternal(null, null);
        }

        /// <summary>
        /// Gets maximum 100 messages sent to your bot, starting from update_id set by offset
        /// </summary>
        /// <param name="offset">First update_id to be downloaded</param>
        /// <returns>On success, the sent <see cref="GetUpdatesResult"/> is returned.</returns>
        public GetUpdatesResult GetUpdates(int offset)
        {
            return GetUpdatesInternal(offset, null);
        }

        /// <summary>
        /// Gets messages sent to your bot, from the begining and maximum number of limit set as parameter
        /// </summary>
        /// <param name="limit">Maximum number of messages to receive. It cannot be more than 100</param>
        /// <returns>Returns a class containing messages sent to your bot</returns>
        public GetUpdatesResult GetUpdates(byte limit)
        {
            return GetUpdatesInternal(null, limit);
        }

        /// <summary>
        /// Gets messages sent to your bot, starting from update_id set by offset, maximum number is set by limit
        /// </summary>
        /// <param name="offset">First update_id to be downloaded</param>
        /// <param name="limit">Maximum number of messages to receive. It cannot be more than 100</param>
        /// <returns>On success, the sent <see cref="GetUpdatesResult"/> is returned.</returns>
        public GetUpdatesResult GetUpdates(int offset, byte limit)
        {
            return GetUpdatesInternal(offset, limit);
        }

        private GetUpdatesResult GetUpdatesInternal(int? offset, byte? limit)
        {
            CheckToken();

            RestRequest request = new RestRequest(string.Format(mGetUpdatesUri, Token), Method.POST);

            if (offset.HasValue)
                request.AddQueryParameter("offset", offset.Value.ToString());
            if (limit.HasValue)
                request.AddQueryParameter("limit", limit.Value.ToString());

            return ExecuteRequest<GetUpdatesResult>(request) as GetUpdatesResult;
        }

        /// <summary>
        /// Checks new updates (sent messages to your bot) automatically. Set CheckInterval property and handle UpdatesReceived event.
        /// </summary>
        public void StartCheckingUpdates()
        {
            CheckToken();

            if (mUpdateTimer == null)
            {
                mUpdateTimer = new Timer(UpdateTimerCallback, null, CheckInterval, Timeout.Infinite);
            }
            else
            {
                mUpdateTimer.Change(CheckInterval, Timeout.Infinite);
            }
        }

        /// <summary>
        /// Stops automatic checking updates
        /// </summary>
        public void StopCheckUpdates()
        {
            mUpdateTimer?.Dispose();
            mUpdateTimer = null;
        }

        /// <summary>
        /// Called when updates received.
        /// </summary>
        /// <param name="updates">The updates</param>
        protected virtual void OnUpdatesReceived(UpdateInfo[] updates)
        {
            TelegramUpdateEventArgs args = new TelegramUpdateEventArgs(updates);
            UpdatesReceived?.Invoke(this, args);
        }

        /// <summary>
        /// Called when [get updates error].
        /// </summary>
        /// <param name="exception">The exception.</param>
        protected virtual void OnGetUpdatesError(Exception exception)
        {
            GetUpdatesError?.Invoke(this, new UnhandledExceptionEventArgs(exception, false));
        }

        private void CheckToken()
        {
            if (Token == null)
                throw new Exception("Token is null");
        }

        private void UpdateTimerCallback(object state)
        {
            try
            {
                GetUpdatesResult updates = mLastUpdateId == 0
                    ? GetUpdates()
                    : GetUpdates(mLastUpdateId + 1);

                UpdateReceived(updates);
            }
            catch (Exception ex)
            {
                OnGetUpdatesError(ex);
            }

            mUpdateTimer?.Change(CheckInterval, Timeout.Infinite);
        }

        private void UpdateReceived(GetUpdatesResult updates)
        {
            if (!updates.Ok ||
                updates.Result == null ||
                !updates.Result.Any())
                return;
            
            mLastUpdateId = updates.Result.Last().UpdateId;
            OnUpdatesReceived(updates.Result);
        }
    }
}