# PaintCalRepo

			Automation Framework Steps

1.	Install  Visual Studio Community Version 2017 or higher
2.	Open  URL : https://github.com/ramanargangasani/PaintCalRepo
3.	Clone or download the project.
		1. Create one folder in the local system(LikeC\Exampleproject)
		2. Under above folder create Homapage folder and Package folder
		3. In Homapage folder unzip Homepage.zip
		4. Package folder unzip Package-1.zip and Package-2.zip
		5. Copy PaintCalc.sln into above folder.	
5.	Create Folder on C:\ ExtentReports for the Test Scenario  reports
6.	Run the Homepage.Testcases namespace.
7.	Test report will generate C:\ ExtentReports  folder 
8.	Test case failing because of UI calculation and business doc’s formula both are not same. I.e. automation script fails in the reports.


Automation Script Overview

1.  Created reusable objects and classes or methods using Page object model.
2. Uses POM objects in the test scenarios.
3. Used N-unit Frame work, Extent reports for the solution.
4. No. of rooms hard coded, but we can prepare test data from excel sheet using OLEDB ACE driver.
5. Length, Width and Height parameters hard coded, we can prepare test data from Excel sheet.
6. Amount of Feet to paint calculation was wrong compare to business requirements.
7. Total Gallons required has a issue with Rounding.


Proper Level to improve the UI and QA


1. Reorganize the UI, better understanding to others.
2. Reorganize the Code.
3. Use the Automation Framework.
