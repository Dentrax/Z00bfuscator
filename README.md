<h1 align="center">Z00bfuscator</h1>

[![MIT Licence](https://badges.frapsoft.com/os/mit/mit.svg?v=103)](https://opensource.org/licenses/mit-license.php)
[![Open Source Love](https://badges.frapsoft.com/os/v1/open-source.png?v=103)](https://github.com/ellerbrock/open-source-badges/)
[![Build status](https://ci.appveyor.com/api/projects/status/l2orub6ui0hr1h08/branch/master?svg=true)](https://ci.appveyor.com/project/Dentrax/z00bfuscator/branch/master)
[![Build Status](https://travis-ci.org/Dentrax/Z00bfuscator.svg?branch=master)](https://travis-ci.org/Dentrax/Z00bfuscator)
[![Sourcegraph](https://img.shields.io/badge/view%20on-Sourcegraph-brightgreen.svg)](https://sourcegraph.com/github.com/Dentrax/Z00bfuscator)

**Z00bfuscator is the simple, open-source, cross-platform obfuscator for .NET Assemblies built in .NET Core**

**Warning:** It is an old Obfuscator I made in `2013`. I converted whole project into .NET Core and made new improvements.

Click here for **[.NET Core](https://docs.microsoft.com/en-us/dotnet/core/)**

Click here for **[Mono.Cecil](https://github.com/jbevain/cecil)**

[What It Is](#what-it-is)

[How To Use](#how-to-use)

[Features](#features)

[Requirements](#requirements)

[Dependencies](#dependencies)

[About](#about)

[Collaborators](#collaborators)

[Branches](#branches) 

[Copyright & Licensing](#copyright--licensing)

[Contributing](#contributing)

[Contact](#contact)

## What It Is

**Z00bfuscator is the simple, open-source, cross-platform obfuscator for .NET Assemblies built in .NET Core SDK**

Z00bfuscator teaches you how obfuscating phases works as simplified as possibly. It is designed in the bare-minimum struct.

### Screenshots

Obfuscating
--------------------------

![Obfuscating](https://raw.githubusercontent.com/Dentrax/Z00bfuscator/master/images/ss_cli_test-run.png)

Before
--------------------------

![Before](https://raw.githubusercontent.com/Dentrax/Z00bfuscator/master/images/ss_gui_before.png)

After
--------------------------

![After](https://raw.githubusercontent.com/Dentrax/Z00bfuscator/master/images/ss_gui_after.png)

Example PE
--------------------------

![Example .NET File](https://raw.githubusercontent.com/Dentrax/Z00bfuscator/master/images/ss_gui_simpleif-obfuscated.png)

Example PE Class
--------------------------

![Example .NET File - Class](https://raw.githubusercontent.com/Dentrax/Z00bfuscator/master/images/ss_gui_simpleif-obfuscated-class.png)

## How To Use

1. Clone the project to your computer by executing the following command:
```
$ git clone https://github.com/Dentrax/Z00bfuscator.git
```

2. Navigate to your `Z00bfuscator` folder: 
```
$ cd Z00bfuscator/
```

3. Build the all projects using `dotnet` command
```
$ dotnet build --configuration Release --output ./bin/Release --verbosity quiet
```

* Run this command if you want to test all cases
```
$ dotnet test .\Z00bfuscator.Tests\Z00bfuscator.Tests.csproj
```

4. Run the Z00bfuscator; Obfuscate the [Z00bfuscator.Test](https://github.com/dentrax/Z00bfuscator/tree/master/Z00bfuscator.Test)
```
$ dotnet run --configuration Release --project .\Z00bfuscator\Z00bfuscator.csproj .\Z00bfuscator.Test\bin\Release\Z00bfuscator.Test.dll
```

5. Test the output project; test the both before and after builds
```
$ dotnet run --configuration Release --project .\Z00bfuscator.Test\Z00bfuscator.Test.csproj

$ cp .\Z00bfuscator.Test\bin\Release\Z00bfuscator.Test.runtimeconfig.json .\Z00bfuscator.Test\bin\Release\Obfuscated_Z00bfuscator.Test.runtimeconfig.json

$ dotnet .\Z00bfuscator.Test\bin\Release\Obfuscated_Z00bfuscator.Test.dll
```

Output
--------------------------

![Output](https://raw.githubusercontent.com/Dentrax/Z00bfuscator/master/images/ss_cli_test.png)

## Features

* Obfuscating Fields

* Obfuscating Methods

* Obfuscating Namespaces

* Obfuscating Properties

* Obfuscating Resources

* Obfuscating Types

## Requirements

* You should be familiar with C# programming
* You should be familiar with Mono.Cecil
* You will need a computer on which you have the rights to compile dotnet files

## Dependencies

* .NET 6 SDK (for compiling and testing)
* Mono.Cecil

## About

Z00bfuscator was created to serve three purposes:

**Z00bfuscator teaches you how obfuscating phases works actually as simplified as possibly**

1. To act as a guide to teach how obfuscating phases works using Mono.Cecil

2. To provide a simplest and easiest way to learn things about Mono.Cecil

3. There is a source for you to develop own Obfuscator mechanism in dotNET environment using Mono.Cecil

## Collaborators

**Project Manager** - Furkan Türkal (GitHub: **[Dentrax](https://github.com/dentrax)**)

## Branches

We publish source for the **[Z00bfuscator]** in single rolling branch:

The **[master branch](https://github.com/dentrax/Z00bfuscator/tree/master)** is extensively tested and makes a great starting point. Also tracks [live changes](https://github.com/dentrax/Z00bfuscator/commits/master) by commits.

## Copyright & Licensing

The base project code is copyrighted by Furkan 'Dentrax' Türkal and is covered by single licence.

All program code (i.e. cs, .md) is licensed under MIT License unless otherwise specified. Please see the **[LICENSE.md](https://github.com/Dentrax/Z00bfuscator/blob/master/LICENSE)** file for more information.

* **[Cecil](https://github.com/jbevain/cecil)**
    - `Cecil` is a library to inspect, modify and create .NET programs and libraries.
    - Please see the **[LICENSE.md](https://github.com/jbevain/cecil/blob/master/LICENSE.txt)** file for more information.

**References**

While this repository is being prepared, it may have been quoted from some sources. 

- https://www.mono-project.com

If there is an unspecified source or if you think that I made a copyright infringement, please contact with me.

## Contributing

Please check the [CONTRIBUTING.md](CONTRIBUTING.md) file for contribution instructions and naming guidelines.

## Contact

Z00bfuscator was created by Furkan 'Dentrax' Türkal

 * <https://www.furkanturkal.com>
 
You can contact by URL:
    **[CONTACT](https://github.com/dentrax)**

<kbd>Best Regards</kbd>