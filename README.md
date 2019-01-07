# Logging Demo

Demo code with examples for educational purpose

## Distinction: Logging and Tracing

[Wikipedia on Tracing](https://en.wikipedia.org/wiki/Tracing_%28software%29)

## Guidelines

Reference
- [ASP.NET Core Logging Guidelines](https://github.com/aspnet/Logging/wiki/Guidelines)

### Log-Level

|ASP.NET Core|Serilog    |NLog |log4net*|
|:----------:|:---------:|:---:|:------:|
|Trace       |Verbose    |Trace|        |
|Debug       |Debug      |Debug|Debug   |
|Information |Information|Info |Info    |
|Warning     |Warning    |Warn |Warn    |
|Error       |Error      |Error|Error   |
|Critical    |Fatal      |Fatal|Fatal   |

*) Default configuration. Additional levels available: Trace, Verbose, Notice, Alert, Severe, Emergency 

#### Verbose/Trace

Audience:
- Primarily intended for developers

Situations:
- Interactive investigation during development
- The finest level of verbosity is required

Quality:
- No restrictions (loops, object dumps, SQL)
- May be noisy (repeated information)
- Might contain sensitive information
- No semantic/structured logging

Performance:
- Affects performance

Storage:
- High volume of data ought to be expected
- The information has no long-term value
- A volumen store ought to be used

#### Debug

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

#### Information/Info

Audience:
- Primarily intended for operations

Situations:
- Tracks the general flow of the application
- Startup configuration settings
- Entry and exit points
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

#### Warning/Warn

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

#### Error

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

#### Fatal/Critical

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

### Filtering

Terminology:

|ASP.NET Core|Serilog       |NLog        |log4net    |System.Diagnostics|
|------------|--------------|------------|-----------|------------------|
|Filter      |Filter        |Rule        |Filter     |Switch            |
|Provider    |Sink          |Target      |Appender   |Listener          |
|Category    |Source Context|Logger Name |Logger Name|                  |

## API's

### EWT/Windows Event Log

[FAQ: Common Questions for ETW and Windows Event Log](https://social.msdn.microsoft.com/Forums/en-US/a1aa1350-41a0-4490-9ae3-9b4520aeb9d4/faq-common-questions-for-etw-and-windows-event-log?forum=etw)