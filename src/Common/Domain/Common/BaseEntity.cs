namespace Domain.Common
{
    using System;

    public abstract class BaseEntity
    {
        public string Creator { get; set; }

        public DateTime CreateDate { get; set; }

        public string Modifier { get; set; }

        public DateTime? ModifyDate { get; set; }

    }
}
