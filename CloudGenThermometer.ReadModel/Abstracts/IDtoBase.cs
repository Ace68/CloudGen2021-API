﻿namespace CloudGenThermometer.ReadModel.Abstracts
{
    public interface IDtoBase
    {
        string Id { get; }
        bool IsDeleted { get; }
    }
}