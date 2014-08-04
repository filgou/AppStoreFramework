using System;
using AppStoreFramework.DAL.Interfaces.Logging;

namespace AppStoreFramework.DAL.Implementations.Logging
{
    public class LogEntry : ILogEntry
    {
        private DateTime creationDate;
        private string category;
        private string message;
        private string sender;
        private string details;
        private string tags;

        public DateTime CreationDate
        {
            get { return this.creationDate; }
            set { this.creationDate = value; }
        }

        public string Category
        {
            get { return this.category; }
            set { this.category = value; }
        }

        public string Message
        {
            get { return this.message; }
            set { this.message = value; }
        }

        public string Sender
        {
            get { return this.sender; }
            set { this.sender = value; }
        }

        public string Details
        {
            get { return this.details; }
            set { this.details = value; }
        }

        public string Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }
    }
}
