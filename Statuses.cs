﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json.Serialization;

namespace TodaysMonet
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Statuses
    {
        Break=30,
        Lunch=60,
        Meeting=120
    }
}
