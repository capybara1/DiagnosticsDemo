# Logging Demo

Demo code with examples for educational purpose

## Distinction: Logging and Tracing

[Wikipedia on Tracing](https://en.wikipedia.org/wiki/Tracing_%28software%29)

## Log-Level

Trace
- Primarily intended for developers
- High volume of data ought to be expected
- A volumen store ought to be used
- No restrictions (loops, object dumps)
- May be noisy (repeated information)
- May contain sensitive information
- Affects performance
- Is usually deactivated and only temporarily activated
- No semantic/structured logging

Debug
- Primarily intended for developers
- High volume of data ought to be expected
- A volumen store ought to be used
- Only log inside of loops if the outup scales well
- Affects performance
- Usually not noisy
- May use semantic/structured logging

Info
- Primarily intended for operations

## EWT/Windows Event Log

[FAQ: Common Questions for ETW and Windows Event Log](https://social.msdn.microsoft.com/Forums/en-US/a1aa1350-41a0-4490-9ae3-9b4520aeb9d4/faq-common-questions-for-etw-and-windows-event-log?forum=etw)