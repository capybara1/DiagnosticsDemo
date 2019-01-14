# Diagnostics Demo

Demo code with examples for educational purpose

## Distinction

[Wikipedia on Tracing](https://en.wikipedia.org/wiki/Tracing_%28software%29)

Linux:
- API: [ptrace - process trace](http://man7.org/linux/man-pages/man2/ptrace.2.html)
- Command: [strace - trace system calls and signals](http://man7.org/linux/man-pages/man1/strace.1.html)

Windows:
- [Tracing and Instrumenting Applications](https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/tracing-and-instrumenting-applications)

## Logging and Tracing

### Guidelines

Reference
- [ASP.NET Core Logging Guidelines](https://github.com/aspnet/Logging/wiki/Guidelines)

### Instrumentation

#### Direct Output

.NET API's:
- Debugger Output: [System.Diagnostics.Debug](https://docs.microsoft.com/de-de/dotnet/api/system.diagnostics.debug?view=netframework-4.8)
- Windows Event Log: [System.Diagnostics.EventLog](https://docs.microsoft.com/de-de/dotnet/api/system.diagnostics.eventlog?view=netframework-4.8)
- Tracing for Windows (ETW): [System.Diagnostics.Tracing](https://docs.microsoft.com/de-de/dotnet/api/system.diagnostics.tracing?view=netframework-4.8)

#### Indirect Output

Logging Frameworks<sup>1</sup>:
- Microsoft:
  - [Logging in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.2)
  - [Tracing and Instrumenting Applications](https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/tracing-and-instrumenting-applications) ([System.Diagnostics.Debug](https://docs.microsoft.com/de-de/dotnet/api/system.diagnostics.debug?view=netframework-4.8))
- 3<sup>rd</sup> party:
  - [Serilog](https://serilog.net/)
  - [NLog](https://nlog-project.org/)
  - [Apache log4net](https://logging.apache.org/log4net/)

<small>1) Excerpt of popular frameworks for .NET at the time of writing</small>

##### Log-Level

|ASP.NET Core|Serilog    |NLog |log4net<sup>2</sup>|System.Diagnostics.Trace|PowerShell<sup>3</sup>|
|:----------:|:---------:|:---:|:-----------------:|:----------------------:|:--------------------:|
|Trace       |Verbose    |Trace|                   |                        |Verbose               |
|Debug       |Debug      |Debug|Debug              |Verbose                 |Debug                 |
|Information |Information|Info |Info               |Information             |Information           |
|Warning     |Warning    |Warn |Warn               |Warning                 |Warning               |
|Error       |Error      |Error|Error              |Error                   |Error                 |
|Critical    |Fatal      |Fatal|Fatal              |Critical                |                      |

<small>2) Default configuration. Additional levels available: Trace, Verbose, Notice, Alert, Severe, Emergency</small>

<small>3) Realizes as individuals streams beside `Host`, `Success` and `Progress`. See also [About Redirection](https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.core/about/about_redirection?view=powershell-6)</small>

###### Verbose/Trace

Audience:
- Primarily intended for developers

Situations:
- Interactive investigation during development
- The finest level of verbosity is required
- Verbose output is required on a command line (e.g. `--verbose`/`-v` or `-vv` or `-vv` in case `--debug`/`-d` is not supported)

Quality:
- No restrictions (loops, object dumps, SQL)
- May be noisy (repeated information)
- Entry and exit points of functions
- Might contain sensitive information
- No semantic/structured logging

Performance:
- Affects performance

Storage:
- High volume of data ought to be expected
- The information has no long-term value
- A volumen store ought to be used

###### Debug

Audience:
- Primarily intended for developers

Situations:
- Interactive investigation during development
- Debug output is required on a command line (e.g. `--debug`/`-d` or `--verbose`/`-v` in case the former is not supported)

Quality:
- Usually not noisy
- The output should scale well
- Semantic/structured logging if required

Performance:
- Affects performance

Storage:
- High volume of data ought to be expected
- The information has no long-term value
- A volumen store ought to be used

###### Information/Info

Audience:
- Primarily intended for operations

Situations:
- Tracks the general flow of the application
- Startup configuration settings
- Entry and exit points of significant flow activities
- Changes to the state of the application

Quality:
- Usually not noisy
- The output should scale well
- Semantic/structured logging recommended

Performance:
- Should not affect performance

Storage:
- Low volume of data ought to be expected
- The information has usually long-term value
- A value store ought to be used

###### Warning/Warn

Audience:
- Primarily intended for operations

Situations:
- An abnormal or unexpected event occured, which did not cause execution to stop, but can signify sub-optimal performance or a potential problem for the future e.g. a handled exceptions.
- Usually not be used in libraries where error handling is left to the embedding code (e.g. via exceptions)

Quality:
- Not noisy
- Semantic/structured logging recommended

Performance:
- Should not affect performance

Storage:
- Low volume of data ought to be expected
- The information has usually long-term value
- A value store ought to be used

###### Error

Audience:
- Primarily intended for operations

Situations:
- The flow of execution is stopped due to a failure that requires investigation
- The "2AM rule": if you're on call, do you want to be woken up at 2AM if this condition happens
- Usually not be used in libraries where error handling is left to the embedding code (e.g. via exceptions)

Quality:
- Not noisy
- Semantic/structured logging recommended

Performance:
- Should not affect performance

Storage:
- Low volume of data ought to be expected
- The information has usually long-term value
- A value store ought to be used

###### Fatal/Critical

Audience:
- Primarily intended for operations

Situations:
- Low volume of data ought to be expected
- An unrecoverable application or system crash
- A catastrophic failure that requires immediate attention e.g. data loss
- Usually used close to the entry point of the application

Quality:
- Not noisy
- Semantic/structured logging if required

Performance:
- Should not affect performance

Storage:
- The information has usually long-term value
- A value store ought to be used

##### Filtering

Terminology:

|ASP.NET Core|Serilog       |NLog        |log4net    |System.Diagnostics.Trace|
|------------|--------------|------------|-----------|------------------------|
|Filter      |Filter        |Rule        |Filter     |Switch                  |
|Provider    |Sink          |Target      |Appender   |Listener                |
|Category    |Source Context|Logger Name |Logger Name|                        |

## Quality Assurance

- Since automation of quality assurance is not possible, logging should be subject of a manual inspection during a review

## Profiling

### Characteristcs

- Used during development/staging

### Tutorials

General:
- [Profiling Overview](https://docs.microsoft.com/en-us/dotnet/framework/unmanaged-api/profiling/profiling-overview)
- [Creating a Custom .NET Profiler](https://www.codeproject.com/Articles/15410/%2FArticles%2F15410%2FCreating-a-Custom-NET-Profiler)

## Monitoring

### Characteristics

- Used in production

### Features

- Exploration
- Visualization
- Alerting

### Instrumentation

#### Performance

WMI:
- [Windows Management Instrumentation](https://docs.microsoft.com/en-us/windows/desktop/wmisdk/wmi-start-page)
- [Performance Counters in the .NET Framework](https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/performance-counters)

#### Events

EWT/Windows Event Log:
- [FAQ: Common Questions for ETW and Windows Event Log](https://social.msdn.microsoft.com/Forums/en-US/a1aa1350-41a0-4490-9ae3-9b4520aeb9d4/faq-common-questions-for-etw-and-windows-event-log?forum=etw)

### Telemetrie

Local Network:
- API
  - [WS-Management](https://www.dmtf.org/standards/ws-man) ([Windows Remote Management](https://docs.microsoft.com/en-us/windows/desktop/winrm/portal))
- Service
  - [Beats](https://www.elastic.co/de/products/beats) and [Logstash](https://www.elastic.co/de/products/logstash) in an [ELK-Stack](https://www.elastic.co/de/elk-stack)

Cloud:
- [Azure Monitor](https://azure.microsoft.com/de-de/services/monitor/)
- [Google Stackdriver](https://cloud.google.com/stackdriver/)
- [Sentry](https://sentry.io/)
- [Elmah](https://elmah.io/)
- [Graylog GELF](http://docs.graylog.org/en/2.3/pages/gelf.html)
- [KissLog](https://kisslog.net/)

### Clients

Windows Remote Management:
- [Creating WMI Clients](https://docs.microsoft.com/en-us/windows/desktop/wmisdk/creating-wmi-clients)

Elasticsearch:
- [Kibana](https://www.elastic.co/de/products/kibana) in an [ELK-Stack](https://www.elastic.co/de/elk-stack)

Azure:
- [Azure Monitor](https://azure.microsoft.com/de-de/services/monitor/)
