namespace Infrastructure.Services
{
    using System;

    using Application.Common.Interfaces;

    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
