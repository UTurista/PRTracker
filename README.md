# Tracker
[Project Reality](https://www.realitymod.com/) Tracker is a custom solution to allow recording and playback game's sessions.

The tool to playback these files, and the de-facto source-of-truth for the protocol implementation can be found in https://github.com/yossizap/realitytracker/;

This project provides a C# port of the parser.

## Protocol
The original source of information of this chapter are the following links. Any inconsistency and the information indicated there should be the one considered correct.
- https://docs.google.com/spreadsheets/d/1ArciEg1rkG_MHzSYWphje1s071a6kD2ojuD58nVmwAE/edit#gid=0
- https://github.com/yossizap/realitytracker/blob/master/js/protocol.js
- https://github.com/yossizap/realitytracker/blob/master/js/parser.js

### Messages
The protocol is simply composed of Messages, the moment one ends a next one starts, with no special delimiters.
```
<Message1><Message2><Message3>
```

A message always starts with 2 bytes representing the payload length. Then the payload always starts with a single byte representing the payload's type.

#### Example
Given the following data:
```
0x00 0x02 0xF1 0x10 0x00 0x01 0xF0
```
1. We start by consuming 2 bytes: `0x00 0x02`
2. This tells us size of the payload of the first message: `0xF1 0x10`
3. The first byte of the payload, `0xF1`, tells us the type of the message: `TickMessage`
4. Each message type defines the order and bytes that follow, in this case, the remaining byte, `0x10`, represents the in-game elapsed time since last tick message.
5. Since all bytes of the payload are consumed we go back to consume 2 bytes to know the next payload's length: `0x00 0x01`
6. A single byte, which is just the message type, `0xF0`, with no extra information.
