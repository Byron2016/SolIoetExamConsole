# A .NET-5 console application to analize employee concurrency.

## Overview

As part of the hiring process, a company requires me to complete a programming exercise to assess my skills.

**Evaluation exercise**
The company ACME offers their employees the flexibility to work the hours they want. But due to some external circumstances they need to know what employees have been at the office within the same time frame

The goal of this exercise is to output a table containing pairs of employees and how often they have coincided in the office.

**INPUT FILE EXAMPLE**

RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00- 21:00
ASTRID=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00
ANDRES=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00

- Text file (.txt)
- Name of an employee 
- equal sign
- Schedule they worked,
  - Day name (valid inputs: MO,TU,WE,TH,FR,SA,SU)
  - Colon
  - Time begin and time end indicating the time and hours, in this format: 10:00-12:00
  - Each day schedule must be separated by a comma 

**OUTPUT EXAMPLE**

OUTPUT:
ASTRID-RENE: 2
ASTRID-ANDRES: 3
RENE-ANDRES: 2

- Output on consule window
- Name one - Name two : coincided in the office number.

- - -
## Architecture

- A Visual Studio 2019 console application
- .NET 5
- It use build-in dependency injection
- Repository pattern
- An approach to MVC architecture. It is divided in two projects.
  - Main project has a controller that control all interactions.
  - Thre are a library project that has de bussiness logic.
  - There are not a separated visual layer, this exist into a method inside controller.
- - -
## Approach and methodology

- To solve this exercise, the input and output requirements were first analyzed. Subsequently, some validations were carried out on the information input file, if there are errors in this file the process continues and it only displays the errors found on the screen, these lines with errors are not considered.
- Input file is converted to an object with all the information, then through a double loop the matches are checked. and displayed on the screen.

- - -
## How to run this sample

**Step 1: Clone or download this repository**

From your shell or command line:

```Shell
git clone https://github.com/Byron2016/SolIoetExamConsole.git
```

or download and exact the repository .zip file.

**Step 2: Build solution and run**

If you prefer to use your shell or command line: 

Inside SolIoetExamConsole directory, from your shell or command line:

```Shell
dotnet build
```

```Shell
dotnet run -p ./ConsoleApp2
```

If you prefer to use Visual Studio 2019: 

- Select Build Solution from Build menu.
- Press F5 to run it. 

## How to use this sample

- Run solution ([Build solution and run](#Step-2:-Build-solution-and-run))
- Enter full path of file to be analyzed and press enter.
```Shell
Please enter File Name (Full Path)
Or press enter to accept: C:\temp\Ioet.txt
```
```Shell
Example of input file for this tutorial.
```
```Shell
RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00- 21:00
ASTRID=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00
ANDRES=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00

Santy=XX10:00-12:00,THaz:00-14:00,SU20:00-21:00
Byron=MO   :00-12:00,THaz:00-14:00,SU20:00-21:00
sofy=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU :00- 21:00
```
- Wait for result.
```Shell
**********************

THERE ARE ERRORS INTO INPUT FILE:

Processed file: C:\temp\Ioet.txt

Line number: 2 Line content: Santy=XX10:00-12:00,THaz:00-14:00,SU20:00-21:00
Line number: 3 Line content: Byron=MO   :00-12:00,THaz:00-14:00,SU20:00-21:00
Line number: 4 Line content: sofy=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU :00- 21:00

**********************

OUTPUT:

Processed file: C:\temp\Ioet.txt

RENE - ASTRID:2
RENE - ANDRES:2
ASTRID - ANDRES:3

**********************

Thanks for use this program.

```
- - -

## How to publish

```Shell
dotnet publish </c/..../SolIoetExamConsole.sln> -c Debug -r win10-x64 --self-contained false -o </c/..../Pubhish> 
```
```Shell
dotnet ConsoleApp2.dll
```
