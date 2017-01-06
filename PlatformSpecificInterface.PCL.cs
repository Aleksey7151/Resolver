﻿using System;
using DependencyResolver;

public interface IPlatformSpecific
{
    string GetString();
}

public class Model
{
    private IPlatformSpecific _platform;

    public Model()
    {
        _platform = Resolver.Get<IPlatformSpecific>();  // Returns platform specific implementator for  IPlatformSpecific interface
    }

    public string InvokePlatformSpecific()      
    {
        return _platform.GetString();
    }
}
