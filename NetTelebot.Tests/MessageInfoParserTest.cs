﻿using System;
using NetTelebot.Tests.TypeTestObject;
using NUnit.Framework;
using Newtonsoft.Json.Linq;


namespace NetTelebot.Tests
{
    [TestFixture]
    internal class MessageInfoParserTest
    {
        private static readonly JObject mMinimumMessageInfoField =
            MessageInfoObject.GetMinimumMessageInfoField(-1147483648, 0,
                1049413668, "Test");

        /// <summary>
        /// Test for <see cref="MessageInfo.Audio"/> parse field.
        /// </summary>
        [Test]
        public static void MessageInfoAudioTest()
        {
            const string fileId = "100";
            const int duration = 100;
            const string performer = "performerTest";
            const string title = "AudioInfo";
            const string mimeType = "mimeTypeTest";
            const int fileSize = 10;

            dynamic MessageInfoAudio = mMinimumMessageInfoField;

            MessageInfoAudio.audio = AudioInfoObject.GetObject(fileId, duration, performer,
                title, mimeType, fileSize);

            MessageInfo messageInfo = new MessageInfo(MessageInfoAudio);

            //test MessageInfo.Audio
            Assert.AreEqual(messageInfo.Audio.FileId, fileId);
            Assert.AreEqual(messageInfo.Audio.Duration, duration);
            Assert.AreEqual(messageInfo.Audio.Performer, performer);
            Assert.AreEqual(messageInfo.Audio.Title, title);
            Assert.AreEqual(messageInfo.Audio.MimeType, mimeType);
            Assert.AreEqual(messageInfo.Audio.FileSize, fileSize);

            Console.WriteLine(MessageInfoAudio);
        }

        /// <summary>
        /// Test for <see cref="MessageInfo.Contact"/> parse field.
        /// </summary>
        [Test]
        public static void MessageInfoContactTest()
        {
            const string phoneNumber = "8080808080";
            const string firstName = "Test Name";
            const string lastName = "Test Last Name";
            const string userId = "0545006540";

            dynamic MessageInfoContact = mMinimumMessageInfoField;

            MessageInfoContact.contact = ContactInfoObject.GetObject(phoneNumber, firstName, lastName,
                userId);

            MessageInfo messageInfo = new MessageInfo(MessageInfoContact);

            //test MessageInfo.Contact
            Assert.AreEqual(messageInfo.Contact.PhoneNumber, phoneNumber);
            Assert.AreEqual(messageInfo.Contact.FirstName, firstName);
            Assert.AreEqual(messageInfo.Contact.LastName, lastName);
            Assert.AreEqual(messageInfo.Contact.UserId, userId);

            Console.WriteLine(MessageInfoContact);
        }

        [Test]
        public static void DeleteChatPhotoParserTest()
        {
            //check MessageInfo witout field [delete_chat_photo]
            dynamic DeleteChatPhoto = mMinimumMessageInfoField;
            MessageInfo messageInfo = new MessageInfo(DeleteChatPhoto);
            Assert.False(messageInfo.DeleteChatPhoto);

            //check MessageInfo with field [delete_chat_photo: true] 
            DeleteChatPhoto.delete_chat_photo = true;
            messageInfo = new MessageInfo(DeleteChatPhoto);
            Assert.True(messageInfo.DeleteChatPhoto);

            Console.WriteLine(DeleteChatPhoto);
        }

        [Test]
        public static void GroupChatCreatedParserTest()
        {
            //check MessageInfo witout field [group_chat_created]
            dynamic GroupChatCreated = mMinimumMessageInfoField;
            MessageInfo messageInfo = new MessageInfo(GroupChatCreated);
            Assert.False(messageInfo.GroupChatCreated);

            //check MessageInfo with field [group_chat_created: true] 
            GroupChatCreated.group_chat_created = true;
            messageInfo = new MessageInfo(GroupChatCreated);
            Assert.True(messageInfo.GroupChatCreated);

            Console.WriteLine(GroupChatCreated);
        }

        [Test]
        public static void SuperGroupChatCreatedParserTest()
        {
            //check MessageInfo witout field [group_chat_created]
            dynamic SuperGroupChatCreated = mMinimumMessageInfoField;
            MessageInfo messageInfo = new MessageInfo(SuperGroupChatCreated);
            Assert.False(messageInfo.SuperGroupChatCreated);

            //check MessageInfo with field [group_chat_created: true] 
            SuperGroupChatCreated.supergroup_chat_created = true;
            messageInfo = new MessageInfo(SuperGroupChatCreated);
            Assert.True(messageInfo.SuperGroupChatCreated);

            Console.WriteLine(SuperGroupChatCreated);
        }

        [Test]
        public static void ChannelChatCreatedParserTest()
        {
            //check MessageInfo witout field [group_chat_created]
            dynamic ChannelChatCreated = mMinimumMessageInfoField;
            MessageInfo messageInfo = new MessageInfo(ChannelChatCreated);
            Assert.False(messageInfo.ChannelChatCreated);

            //check MessageInfo with field [group_chat_created: true] 
            ChannelChatCreated.channel_chat_created = true;
            messageInfo = new MessageInfo(ChannelChatCreated);
            Assert.True(messageInfo.ChannelChatCreated);

            Console.WriteLine(ChannelChatCreated);
        }

        [Test]
        public static void MigrateToChatIdParserTest()
        {
            //check MessageInfo witout field [group_chat_created]
            dynamic MigrateToChatId = mMinimumMessageInfoField;
            MessageInfo messageInfo = new MessageInfo(MigrateToChatId);
            Assert.AreEqual(messageInfo.MigrateToChatId, 0);

            //check MessageInfo with field [group_chat_created: true] 
            MigrateToChatId.migrate_to_chat_id = 14881488;
            messageInfo = new MessageInfo(MigrateToChatId);
            Assert.AreEqual(messageInfo.MigrateToChatId, 14881488);

            Console.WriteLine(MigrateToChatId);
        }

        [Test]
        public static void MigrateFromChatIdParserTest()
        {
            //check MessageInfo witout field [group_chat_created]
            dynamic MigrateFromChatId = mMinimumMessageInfoField;
            MessageInfo messageInfo = new MessageInfo(MigrateFromChatId);
            Assert.AreEqual(messageInfo.MigrateFromChatId, 0);

            //check MessageInfo with field [group_chat_created: true] 
            MigrateFromChatId.migrate_from_chat_id = 14881488;
            messageInfo = new MessageInfo(MigrateFromChatId);
            Assert.AreEqual(messageInfo.MigrateFromChatId, 14881488);

            Console.WriteLine(MigrateFromChatId);
        }

        [Test]
        public static void MinMessageInfoTest()
        {
            dynamic minMessageInfoField = mMinimumMessageInfoField;

            MessageInfo messageInfo = new MessageInfo(minMessageInfoField);

            Assert.AreEqual(messageInfo.MessageId, -1147483648);
            Assert.AreEqual(messageInfo.DateUnix, 0);
            Assert.AreEqual(messageInfo.Date, new DateTime(1970, 1, 1).ToLocalTime());
            Assert.AreEqual(messageInfo.Chat.Id, 1049413668);

            Console.WriteLine(minMessageInfoField);
        }


        /// <summary>
        /// Test for <see cref="MessageInfo.Sticker"/> parse field.
        /// </summary>
        [Test]
        public static void MessageInfoStickerTest()
        {
            //todo Do similar tests for all fields MessageInfo
            const string fileId = "100";
            const int width = 100;
            const int height = 100;
            const string emoji = "emoji";
            const int fileSize = 10;

            dynamic MessageInfoSticker = mMinimumMessageInfoField;

            MessageInfoSticker.sticker = StickerInfoObject.GetObject(fileId, width, height,
                PhotoSizeInfoObject.GetObject(fileId, width, height, fileSize), emoji, fileSize);

            MessageInfo messageInfo = new MessageInfo(MessageInfoSticker);

            //test MessageInfo.Sticker
            Assert.AreEqual(messageInfo.Sticker.FileId, fileId);
            Assert.AreEqual(messageInfo.Sticker.Width, width);
            Assert.AreEqual(messageInfo.Sticker.Height, height);
            Assert.AreEqual(messageInfo.Sticker.Emoji, emoji);
            Assert.AreEqual(messageInfo.Sticker.FileSize, fileSize);

            //test MessageInfo.Sticker.Thumb
            Assert.AreEqual(messageInfo.Sticker.Thumb.FileId, fileId);
            Assert.AreEqual(messageInfo.Sticker.Thumb.Width, width);
            Assert.AreEqual(messageInfo.Sticker.Thumb.Height, height);
            Assert.AreEqual(messageInfo.Sticker.Thumb.FileSize, fileSize);

            Console.WriteLine(MessageInfoSticker);

        }

        /// <summary>
        /// Test for <see cref="MessageInfo.Video"/> parse field.
        /// </summary>
        [Test]
        public static void MessageInfoVideoTest()
        {
            const string fileId = "100";
            const int width = 1000;
            const int height = 10000;
            const int duration = 1000;
            const string mimeType = "mimeType";
            const int fileSize = 100;

            dynamic MessageInfoVideo = mMinimumMessageInfoField;

            MessageInfoVideo.video = VideoInfoObject.GetObject(fileId, width, height, duration,
                PhotoSizeInfoObject.GetObject(fileId, width, height, fileSize), mimeType, fileSize);

            MessageInfo messageInfo = new MessageInfo(MessageInfoVideo);

            //test MessageInfo.Video
            Assert.AreEqual(messageInfo.Video.FileId, fileId);
            Assert.AreEqual(messageInfo.Video.Width, width);
            Assert.AreEqual(messageInfo.Video.Height, height);
            Assert.AreEqual(messageInfo.Video.Duration, duration);
            Assert.AreEqual(messageInfo.Video.MimeType, mimeType);
            Assert.AreEqual(messageInfo.Video.FileSize, fileSize);

            //test MessageInfo.Video.Thumb
            Assert.AreEqual(messageInfo.Video.Thumb.FileId, fileId);
            Assert.AreEqual(messageInfo.Video.Thumb.Width, width);
            Assert.AreEqual(messageInfo.Video.Thumb.Height, height);
            Assert.AreEqual(messageInfo.Video.Thumb.FileSize, fileSize);

            Console.WriteLine(MessageInfoVideo);
        }

        /// <summary>
        /// Test for <see cref="MessageInfo.Location"/> parse field.
        /// </summary>
        [Test]
        public static void MessageInfoLocationTest()
        {
            const float longitude = 1000;
            const float latitude = 1000;

            dynamic MessageInfoLocation = mMinimumMessageInfoField;

            MessageInfoLocation.location = LocationInfoObject.GetObject(longitude, latitude);

            MessageInfo messageInfo = new MessageInfo(MessageInfoLocation);

            Assert.AreEqual(messageInfo.Location.Latitude, latitude);
            Assert.AreEqual(messageInfo.Location.Longitude, longitude);
        }
    }
}