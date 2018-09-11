#Mars Imaging


##This is an Interview project for WatchGuardVideo.com

*This project should not have been accepted with the current defined scope.  There are questions that need to be asked.*
1.	The purpose of the text file was unclear.  
	- does the text file provide download information?	
	- Should the text file include a field for which rover?
	- Should the text file also allow for Sol selection?
2.	With the date provided, should ALL pictures for all Rovers be downloaded?
3.	Should this application be an API or stand alone Application.
4.	what should the jpeg filenames be?
5.	what folder structure should the downloaded files be written to?
6.	Does the API download a single picture or all pictures for a specific day/rover.  
7.  No Defined SLA for Mars API interface testing.

Based on these questions, the following assumptions were made
1.	The text file will specify the date and rover for images to download.
	The following format will be date|rover with cr/lf terminating the end of each line.
	Currently the text file does not allow for Mars Sol date.
2.	Each row in the text file can specify a specific rover or all rovers.  See below for furture explination
3.	This project will mix a console application and API to accomplish the assumed goals
	the console application will be an easy entry point to call the API.
  1 Retrieve Rover images by rover and earth date
  2 Retrieve Rover images by rover and Mars Sol
  3 Retrieve Images for All Rovers by EarthDate
  4 Retrieve Images for All Rovers by Mars Sol
  5 Retrieve images by text file.
    - Select 5, then drag the text file to the command window and press enter.
4.	Use name provided by the Mars API as the name of the downloaded Jpg, with jpg extension
5.	the folder structure is images/{rover}/{date} under the kfe.mars folder of the project.  
6.	The API downloads all pictures for a specific Rover for a Specific date(Earth or Sol).
7.  The performance is going to be limited by the Mars API interface.  
  1 The documentation from the Mars API states that all requests will be limited to 5000 requests/hour.  
  2 Currently, with the provided test data, there are less than 20 requests being made using the test file.

##Notes:
* Visual Studio 2017 .Net Core 2

######kfe.Mars.Console

Written .Net Core 

The solution will launch both the kfe.Mars API and the kfe.Mars.Console application

A simple command line interface to execise parts of the kfe.Mars API.

- In the TestFile folder of the project is a pipe delimited file that will drive the downlaods
  - Field 1 is the date (currenly only Earth date)
  - Field 2 is the Rover.  
    - Curiosity, 
	- Opportunity
	- Spirit 
	- All

######kfe.Mars

Written is a .Net Core 2 project so it can be deployed on many platforms:
1 AWS
  - Docker Container
  - EC2 AMI
2 Azure
  - Currently no experience deploying to Azure
3 Raspberry Pi 3 running either Linux or Windows IOT Core (small bonus)
  - Using Docker Container with Linux
  - Windows IOT deployment tools
  - can bring a Pi if a demonstration is requested.

project is the entry point of the actual API and utlizes swagger for an quick UI to test the endpoints.
- There are three endpoints for the API
  - api/v1/marsImaging/earthDate
    * downloads all images for specified rover for specified Date
  - api/v1/marsImaging/solDate
	* downloads all images for specified rover for Mars Sol (Mars Day)
  - api/v1/marsImaging/rovers
	* Gets Photo manifest of all current photos based on rover selection.

##Unit Tests
- Using XUnit
- Currently only running tests against Services project
##Static analysis
- SonarQube: will include report in kfe.Mars project folder

##Performance Test
- No current performance testing

