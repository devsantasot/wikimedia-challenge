# Diego Santamaria - ProgramingChallenge
## Description

The main purpose of this project is to evaluate my skills in object-oriented programming and design.

I've use TPL Dataflow in C# Net Core to processing specific pageviews for Wikipedia site provide by The Wikimedia Foundation.
The pageviews can be downloaded in gzip format and are aggregated per hour per page. 
Each hourly dump is approximately 50MB in gzipped text file and is somewhere between 100MB and 250MB in size unzipped.

* File’s location: https://dumps.wikimedia.org/other/pageviews/
* Sample file: https://dumps.wikimedia.org/other/pageviews/2015/2015-05/pageviews-20150501-010000.gz
* Technical documentation: https://wikitech.wikimedia.org/wiki/Analytics/Data_Lake/Traffic/Pageviews

### Requirements: 
1. Do not use any relative database in your code.  
2. Get data for last 5 hours.
3. Calculate by the code the following SQL statement (ALL_HOURS table represent all files)
_______________________________________________________________

[SQL statement.txt](https://github.com/dsantasot/DS_ProgramingChallenge/files/6369860/SQL.statement.txt)
_______________________________________________________________

[Output Example.txt](https://github.com/dsantasot/DS_ProgramingChallenge/files/6369853/Output.Example.txt)
_______________________________________________________________

### How It Works
This command line application has the following capabilities: 

1. Gets the download URLs built using the parameters specified in the <code>appsettings.json</code> file and then downloads the file to the workspace (local disk).
2. Unzip the file to your workspace (local disk).
3. Reads the unzipped file and processes it line by line to get a list of objects. These objects are filtered using linq statements to reduce the size of all data. The result is saved in a new file in the workspace (local disk). When all the files have been processed and saved in the workspace, the result is combined into a single file. Finally this file (smaller than the previous ones) is transformed into a list of objects that is used as a data source to execute new Linq statements.
4. Finally, print the result of the analysis.

### Config
View of <code>appsettings.json</code>
```json
{
  "LastHoursRequest": 5,
  "BaseURLDownload": "https://dumps.wikimedia.org/other/pageviews",
  "FileRuteFormat": "yyyy/yyyy-MM",
  "FileNameFormat": "'pageviews'-yyyyMMdd-HH'0000.gz'",
  "FilesWorkspacePath": "C:\\tmp",
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Information",
        "System": "Warning"
      }
    }
  }
}
```

1. <code>LastHoursRequest</code> node represent the number of hours for the request. According to the requirement, it is 5 hours.
2. <code>BaseURLDownload</code> node represents the base url for the request.
3. <code>FileRuteFormat</code> node represents the expected format of the rute for the request.
4. <code>FileNameFormat</code> node represents the expected format of the file for the request.
5. <code>FilesWorkspacePath</code> node represents the directory of the workspace for the application.
6. <code>Serilog</code> section represents the configuration for the Serilog tool.


* Author : Diego Santamaria Sotelo
* Date   : 24/04/2021
