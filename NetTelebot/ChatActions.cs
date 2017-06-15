﻿namespace NetTelebot
{
    /// <summary>
    /// Use this enum when you need to tell the user that something is happening on the bot's side. The status is set for 5 seconds or less (when a message arrives from your bot, Telegram clients clear its typing status).
    /// Type of action to broadcast. Choose one, depending on what the user is about to receive
    /// </summary>
    public enum ChatActions
    {
        /// <summary>
        /// User is about to receive text message 
        /// </summary>
        Typing,
        /// <summary>
        /// User is about to receive photos
        /// </summary>
        Upload_photo,
        /// <summary>
        /// User is about to receive video
        /// </summary>
        Record_video,
        /// <summary>
        /// User is about to receive video
        /// </summary>
        Upload_video,
        /// <summary>
        /// User is about to receive audio files
        /// </summary>
        Record_audio,
        /// <summary>
        /// User is about to receive audio files
        /// </summary>
        Upload_audio,
        /// <summary>
        /// User is about to receive general files
        /// </summary>
        Upload_document,
        /// <summary>
        /// User is about to receive location data
        /// </summary>
        Find_location,
        /// <summary>
        /// User is about to receive video notes
        /// </summary>
        Record_video_none,
        /// <summary>
        /// User is about to receive video notes
        /// </summary>
        Upload_video_note
    }
}
