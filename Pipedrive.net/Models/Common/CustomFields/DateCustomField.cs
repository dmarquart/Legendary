﻿using System;

namespace Pipedrive.CustomFields
{
    public class DateCustomField : ICustomField
    {
        public DateTime Value { get; set; }

        public DateCustomField(DateTime value)
        {
            Value = value;
        }
    }
}
