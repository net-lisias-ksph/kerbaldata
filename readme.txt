What is Kerbal Data?
Want to write an external application to process KSP data but opened a file and found working with the data is a pain? Don't want to deal with upgrades breaking your application? Want to convert your KSP flight data to other useful formats? If you answered yes to any of these then the KerbalData API is for you. This simple C# API allows you quick and easy access to your KSP data files (save, craft, config and part) and allows you to manipulate and store that data back into KSP format (and eventually any format you like). This API allows developers to:

Load/Edit/Save KSP Save, Craft (both global and under a specific save), Part and game settings.
Scan a KSP install for all available data files (and makes them available through a single line of code)
Convert KSP data to/from JSON format (use the huge ecosystem JSON tools to manage your data)
Manage, edit, save to/from JSON or KSP format
De-serializes data into usable object structure
Automatically backs up changed files, keeps a copy of original data to restore live object.
Save Development Time : Time Savings
Do simple popular actions
Clear all debris and unknown items from a save
Put any craft currently in game (on the pad or in orbit) in orbit around any body
Refuel any or all craft in your save with a single command (fills any resource to max)
more to come (looking for suggestions)...

For support, docs and more information: https://kerbaldata.codeplex.com
Community Thread: http://forum.kerbalspaceprogram.com/showthread.php/37728-0-18

Release Notes:
0.3.2 
-----------------
- Adding tests for data helper methods on models.


- Additions to Storable and KspData tests. Multiple bug fixes around standalone file load/save use cases.


- added code coverage and reporting, added additional tests for object CRUD operations, fixed bug with Delete() on StorableObjects{T}


- Updated to general processing tests. Fixed bug in DataConverter for comments with special chars.


- Moved around test data to match install folder locations. Added initial tests for KerbalData class.


- Switching unit tests over to nUnit 2.6 to allow for wider testing on more platforms


- Minor updates to remove default references to "kerbalData" configuration section. Using hard-coded default config for now to make things easier for end users.


- General cleanup, added docs, fixed doc spelling.


- Bugfix for loading a single files without context. Added unit tests for single file load.



0.3.1 
-----------------
- Additions to IStorableObject implementation to provide more flexibility to custom workflows.


- Fixes to default config and init of Craft hangars in Save files.


- Multiple Bug Fixes/Enhancements to support file Export with a loaded registry.


- Minor updates to Orbit class. Now has a public property named "Altitude" this now works with the Body property and the AltitudeOffsets to automatically calculate and update the sma when changing either the Body or the Altitude values.


- Minor updates based on KerbalEdit development. Stripped repo interface and implementation of methods with "out" parameters. Data reversion will be preformed from the file rather than keeping a copy of the data in order to reduce memory usage.


- Added base DisplayName property to IKerbalDataObject. This value can be used by UI's to always get a friendly and somewhat short name for display to end users.

0.3.0 
-----------------
- Update to WPF example to include 2-way binding and Save button on all files.


- Updates to code to support .NET4/3.5 and Mono differences in latest code changes. Updated project meta-data so debug symbols build on all test project builds. NOTE: Tests do not work under Mono. I will need to switch to nUnit in order to provide mono support in unit tests. This will be done in an upcoming release as a side task.


- Grabbed ObservableDictionary implementation from freely available Dr. WPF article: http://drwpf.com/blog/2007/09/16/can-i-bind-my-itemscontrol-to-a-dictionary/ Replaced usage of Dictionary<string, JToken> with ObservableDictionary<string, JToken>. This will remain provided testing with the patterns works well.


- Initial wire-in of INotifyPropertyChanged on all models and related properties. Still need to workout the flow for updates to base class dictionaries for unmapped properties.


- Documentation cleanup and update. Added top level topic placeholders for examples and additional docs. Collapsed projects down now that dynamic load testing is completed. Re-arranged the model classes to their own namespace ("Models") this is to prevent name conflicts when developers chose to use their own model sets with KerbalData


- Implementation of higher level patterns for custom models and usage of configurable repos and processors.


- Adding configurable data processor. Example configuration can be found in KerbalData.Tests. Top level consumer API assumes default configurations for top level models and Newtonsoft.Linq.JObject


- Closer to formal serialization pattern. At this point all that should be missing is a configurable reader/writer implementation. Serilaizer Interfaces changed to something more sane and the internal converter role has been formalized. All major interfaces are provided through factory/injection patterns.


- Added Sandcastle to build system, added additional documentation to classes.


0.2.0 Release Notes

- Updates to provide support for .NET 3.5,4.0 and Mono 2.10.x

- final updates before release, expanded WPF example, tweaked part model


- Updating editor to leverage KerbalData file scanning/loading. Added folder select dialog. User selects a ksp install and all available data files are displayed in a series of list boxes. More to come...


- Updated existing examples to work with latest API changes. Moved KSPEditor to it's own folder.


- API documentation, switched project options to generate reference XML on build. Adding more models and properties.


- Fixed bug in de-serilization causing issue with loading collections. Including latest model updates around part and config data.

- Documentation update


- Completed mapping all top level model to KerbalData.


- Wire in top level part, settings and craft for top level craft folders.


- Re-worked serializer code for better future migration and support for comments/accurate file line writing. Serilaizer still strips extra carriage returns. Added game parts top level classes. Broke serializers and data repository implementations into separate projects as first stage of isolating core api.


- moved base SaveFile object files into thier own folder. Added data loading infrastrcture with simple Repo/Factory pattern. Wrapped up initial implmentation of top level kerbal data class. API is now Install aware. Simply create a new KerbalData object passing in your base install path. Available saves will be loaded as requested from the collection ex var kd = new KerbalData("C:\\KSP\\"); kd.Saves["mysavename"].Game.Title = "HELLO WORLD!"; kd.Saves["mysavename"].Save();


- Minor bug in refactored unmappedpropertiesconverter fixed. Adding a few more models.


- Adding models, includes bugfix to properties converter for processing array types.


- Mid-Phase update. Scrapped manager pattern for hopefully more "correct" serialization pattern using a Converter class. Implmented all base functionality along with some additional models.


- Completed Code Review, Added code documentation and license file.


- Moved folders around and added basic build system. For a quick .NET 4.0 build just run build.bat from the command line. Mono and other .net versions to come. Default build is RELEASE.


