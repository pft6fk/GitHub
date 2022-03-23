# This is Website that was made by using .Net core.
In order to run this program, first thing that should be done, is put your credentials inside of constructor of HomeController.

This program makes API calls to the GitHub, gets public repos (only those, which were searched) stores in local database, and then gives you all found repos.
When user search repo that exists in localDb, instead of making request to the GitHub, it takes required data from the Db and represents it to the user
