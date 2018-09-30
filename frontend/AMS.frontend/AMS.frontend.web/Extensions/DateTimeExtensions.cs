﻿using System;

namespace AMS.frontend.web.Extensions
{
    public static class DateTimeExtensions
    {
        #region Public Methods

        public static int GetAge(this DateTime dateOfBirth)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;

            return (a - b) / 10000;
        }

        #endregion Public Methods
    }
}