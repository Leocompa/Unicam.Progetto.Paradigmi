﻿namespace Paradigmi.Models.Entities;

public class Address
{
    public string Città { get; set; } = String.Empty;
    public string Cap { get; set; } = String.Empty;
    public string Via { get; set; } = String.Empty;
    public int Civico { get; set; }
    
}