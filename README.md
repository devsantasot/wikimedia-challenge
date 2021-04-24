# DS_ProgramingChallenge
Diego Santamaria Programming Challenge 

The main purpose of this project is to evaluate my skills in object-oriented programming and design.

I've use TPL Dataflow in C# Net Core to processing specific pageviews for Wikipedia site provide by The Wikimedia Foundation.
The pageviews can be downloaded in gzip format and are aggregated per hour per page. 
Each hourly dump is approximately 50MB in gzipped text file and is somewhere between 100MB and 250MB in size unzipped.

File’s location: https://dumps.wikimedia.org/other/pageviews/
Sample file: https://dumps.wikimedia.org/other/pageviews/2015/2015-05/pageviews-20150501-010000.gz
Technical documentation: https://wikitech.wikimedia.org/wiki/Analytics/Data_Lake/Traffic/Pageviews

Requiriments: 
1. Do not use any relative database in your code.  
2. Get data for last 5 hours.
3. Calculate by the code the following SQL statement (ALL_HOURS table represent all files)
_______________________________________________________________

[SQL statement.txt](https://github.com/dsantasot/DS_ProgramingChallenge/files/6369860/SQL.statement.txt)
_______________________________________________________________

[Output Example.txt](https://github.com/dsantasot/DS_ProgramingChallenge/files/6369853/Output.Example.txt)
_______________________________________________________________

This command line application has the following capabilities: 

 1. Download the file to workspace (local disk).
    1.1. Get the URLs to download.
    1.2. Download the resource.
 2. Decompress the file  to workspace (local disk).
 3. Process all data in a unify file.
    3.1. Read the file and transform the data to reduce the size. 
    3.2. Unify all processed files into one (máx 600MB).
    3.3. Process all data using the unify file.
 4. Print the result of the analysis.
 
 Author : Diego Santamaria Sotelo
 Date   : 24/04/2021
