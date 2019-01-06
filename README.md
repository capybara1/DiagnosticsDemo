# Logging Demo

Demo code with examples for educational purpose

## Distinction: Logging and Tracing

[Wikipedia on Tracing](https://en.wikipedia.org/wiki/Tracing_%28software%29)

## Guidelines

Reference
- [ASP.NET Core Loggin Guidelines](https://github.com/aspnet/Logging/wiki/Guidelines)

### Log-Level

Trace
- Audience
  - Primarily intended for developers
- Situations
  - Interactive investigation during development
- Storage
  - High volume of data ought to be expected
  - The information has no long-term value
  - A volumen store ought to be used
- Performance
  - Affects performance
- Quality
  - No restrictions (loops, object dumps, SQL)
  - May be noisy (repeated information)
  - Might contain sensitive information
  - No semantic/structured logging

Debug
- Audience
  - Primarily intended for developers
- Situation
  - Interactive investigation during development
- Storage
  - High volume of data ought to be expected
  - The information has no long-term value
  - A volumen store ought to be used
- Performance
  - Affects performance
- Quality
  - Usually not noisy
  - The output should scale well
  - May use semantic/structured if value storage depending on design 

Info
- Audience
  - Primarily intended for operations
- Situations
  - Tracks the general flow of the application
    - Startup configuration settings
    - Entry and exit points
    - Changes to the state of the application
- Storage
  - Low volume of data ought to be expected
  - The information has usually long-term value
  - A value store ought to be used
- Performance
  - Should not affect performance
- Quality
  - Usually not noisy
  - The output should scale well
  - May use semantic/structured logging (depending on design)

Warning
- Audience
  - Primarily intended for operations
- Situations
  - An abnormal or unexpected event occured, which did not cause execution to stop, but can signify sub-optimal performance or a potential problem for the future e.g. a handled exceptions.
- Storage
  - Low volume of data ought to be expected
  - The information has usually long-term value
  - A value store ought to be used
- Performance
  - Should not affect performance
- Quality
  - Not noisy
  - May use semantic/structured logging (depending on design)

Error
- Audience
  - Primarily intended for operations
- Situations
  - The flow of execution is stopped due to a failure that requires investigation
- Storage
  - Low volume of data ought to be expected
  - The information has usually long-term value
  - A value store ought to be used
- Performance
  - Should not affect performance
- Quality
  - Not noisy
  - May use semantic/structured logging (depending on design)

Critical
- Audience
  - Primarily intended for operations
- Situations
  - Low volume of data ought to be expected
  - An unrecoverable application or system crash
  - A catastrophic failure that requires immediate attention e.g. data loss
- Storage
  - The information has usually long-term value
  - A value store ought to be used
- Performance
  - Should not affect performance
- Quality
  - Not noisy
  - May use semantic/structured logging (depending on design)

## APIs

### EWT/Windows Event Log

[FAQ: Common Questions for ETW and Windows Event Log](https://social.msdn.microsoft.com/Forums/en-US/a1aa1350-41a0-4490-9ae3-9b4520aeb9d4/faq-common-questions-for-etw-and-windows-event-log?forum=etw)