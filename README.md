# IntegraXL
Roland INTEGRA-7 Class Library .NET 6.0

Hobby project, provides the data structures and functionality to build an INTEGRA-7 application.

The idea is to support multiple physical devices, I try to achieve this by using the [ConnectionManager](IntegraXL/Core/IntegraConnectionManager.cs) class. To create a new [Integra](IntegraXL/Integra.cs) instance a connection is required which is obtained using the connection manager's CreateConnection method. The connection requires an ID equal to the physical devices ID. So when a system exclusive message is send the connection filters the messages by this ID.

Furthermore the connection requires a MIDI input and output device which can be any MIDI library as long as it implements the [IMIDIInputDevice](IntegraXL/Interfaces/IMIDIInputDevice) and [IMIDIOutputDevice](IntegraXL/Interfaces/IMIDIOutputDevice) interfaces so you can use your preferred MIDI library as I use the [MidiXL](https://github.com/X-Lars/MidiXL) library in the example. An implementation example can be found [here](https://github.com/X-Lars/IntegraEditorXL/blob/master/IntegraEditorXL/MidiDevices.cs).

With a valid connection a new [Integra](IntegraXL/Integra.cs) instance can be created and a call to the initialization method loads all nescessary data into the [Models](IntegraXL/Models/). The StudioSet and TemporaryTones are the two main models providing submodel properties for almost all INTEGRA-7 editable parameters and are themselves provided by the Integra instance as property.

```
// Create connection
IntegraConnection connection = IntegraConnectionManager.CreateConnection(17, new MidiXLOutputDevice(2), new MidiXLInputDevice(1));

// Create instance
Integra device = new Integra(connection); // Device ID = 17

// Report progress or just wait
var result = await Integra.Initialize();
```

With the above code you should be up and running.

Working to a first release, more to come...

