﻿namespace CarRentalSystem.Application;

public class ApplicationSettings
{
    public ApplicationSettings() => this.Secret = default!;

    public string Secret { get; set; }
}