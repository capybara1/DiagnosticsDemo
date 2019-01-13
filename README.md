# Diagnostics Demo

Demo code with examples for educational purpose
## Distinction

- [Tracing and Instrumenting Applications](https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/tracing-and-instrumenting-applications)
- [Wikipedia on Tracing](https://en.wikipedia.org/wiki/Tracing_%28software%29)

## Logging and Tracing

### Guidelines

Reference
- [ASP.NET Core Logging Guidelines](https://github.com/aspnet/Logging/wiki/Guidelines)

### Instrumentation

Logging Frameworks<sup>1</sup>:
- [Serilog](https://serilog.net/)
- [NLog](https://nlog-project.org/)
- [Apache log4net](https://logging.apache.org/log4net/)
- [Logging in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.2)
- [Tracing and Instrumenting Applications](https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/tracing-and-instrumenting-applications)

<small>1) Excerpt of popular frameworks for .NET at the time of writing</small>

#### Log-Level

|ASP.NET Core|Serilog    |NLog |log4net<sup>2</sup>|
|:----------:|:---------:|:---:|:------:|
|Trace       |Verbose    |Trace|        |
|Debug       |Debug      |Debug|Debug   |
|Information |Information|Info |Info    |
|Warning     |Warning    |Warn |Warn    |
|Error       |Error      |Error|Error   |
|Critical    |Fatal      |Fatal|Fatal   |

<small>2) Default configuration. Additional levels available: Trace, Verbose, Notice, Alert, Severe, Emergency</small>

##### Verbose/Trace

Audience:
- Primarily intended for developers

Situations:
- Interactive investigation during development
- The finest level of verbosity is required

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

##### Debug

Audience:
- Primarily intended for developers

Situations:
- Interactive investigation during development

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

##### Information/Info

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

##### Warning/Warn

Audience:
- Primarily intended for operations

Situations:
- An abnormal or unexpected event occured, which did not cause execution to stop, but can signify sub-optimal performance or a potential problem for the future e.g. a handled exceptions.

Quality:
- Not noisy
- Semantic/structured logging recommended

Performance:
- Should not affect performance

Storage:
- Low volume of data ought to be expected
- The information has usually long-term value
- A value store ought to be used

##### Error

Audience:
- Primarily intended for operations

Situations:
- The flow of execution is stopped due to a failure that requires investigation
- The "2AM rule": if you're on call, do you want to be woken up at 2AM if this condition happens

Quality:
- Not noisy
- Semantic/structured logging recommended

Performance:
- Should not affect performance

Storage:
- Low volume of data ought to be expected
- The information has usually long-term value
- A value store ought to be used

##### Fatal/Critical

Audience:
- Primarily intended for operations

Situations:
- Low volume of data ought to be expected
- An unrecoverable application or system crash
- A catastrophic failure that requires immediate attention e.g. data loss

Quality:
- Not noisy
- Semantic/structured logging if required

Performance:
- Should not affect performance

Storage:
- The information has usually long-term value
- A value store ought to be used

#### Filtering

Terminology:

|ASP.NET Core|Serilog       |NLog        |log4net    |System.Diagnostics|
|------------|--------------|------------|-----------|------------------|
|Filter      |Filter        |Rule        |Filter     |Switch            |
|Provider    |Sink          |Target      |Appender   |Listener          |
|Category    |Source Context|Logger Name |Logger Name|                  |

## Quality Assurance

- Since automation of quality assurance is not possible, logging should be subject of a manual inspection during a review

## Profiling

### Characteristcs

- Used during development/staging

### Tutorials

- [Profiling Overview](https://docs.microsoft.com/en-us/dotnet/framework/unmanaged-api/profiling/profiling-overview)
- [Creating a Custom .NET Profiler](https://www.codeproject.com/Articles/15410/%2FArticles%2F15410%2FCreating-a-Custom-NET-Profiler)

## Monitoring

### Characteristcs

- Used in production

### Instrumentation

#### Performance

- [Windows Management Instrumentation](https://docs.microsoft.com/en-us/windows/desktop/wmisdk/wmi-start-page)
- [Performance Counters in the .NET Framework](https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/performance-counters)

#### Events

##### EWT/Windows Event Log

- [FAQ: Common Questions for ETW and Windows Event Log](https://social.msdn.microsoft.com/Forums/en-US/a1aa1350-41a0-4490-9ae3-9b4520aeb9d4/faq-common-questions-for-etw-and-windows-event-log?forum=etw)
