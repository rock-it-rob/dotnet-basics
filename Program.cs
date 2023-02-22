﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using FileReaders;
using FileWriters;

// Create a Host for DI.
using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(ConfigureServices)
    .Build();

void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<IFixedLengthReader, FixedLengthReader>();
    services.AddSingleton<FileReaderApp>();
    services.AddSingleton<IFixedLengthWriter, FixedLengthWriter>();
    services.AddSingleton<FileWriterApp>();
}

var reader = host.Services.GetService<FileReaderApp>();
var writer = host.Services.GetService<FileWriterApp>();

reader!.Execute();
writer!.Execute();