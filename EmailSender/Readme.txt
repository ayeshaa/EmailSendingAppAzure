# WeatherForecast

EmailSenderApp is a .Net Core application

## Installation

1- Download the project
2- Open in visual studio.
4- The project contains two api endpoints
	1- EmailSender
	endpoint: https://localhost:44389/api/EmailSender/ LocalHost
    endpoint: https://emailsender20220225010929.azurewebsites.net/api/EmailSender/ Live
    Request: Post
	Json Data Sample:
	{
    "EmailSenderInputs": [
       {
            "Key": "848734834387",

            "Email":"anonymous123@gmail.com",

            "Attributes": ["one", "two", "rock"]
        },
        {
            "Key": "876869569345",

            "Email":"anonymous123@gmail.com",

            "Attributes": ["hello", "world", "rock", "map", "location"]
        },
        {

        "Key": "654653643334",

        "Email":"anonymous123@gmail.com",

        "Attributes": ["blues", "rock", "jazz", "rap","classic"]

        },
        {

            "Key": "848734834387",

            "Email":"anonymous890@gmail.com",

            "Attributes": ["uno", "dos"]

        },
        {

            "Key": "876869569345",

            "Email":"anonymous890@gmail.com",

            "Attributes": ["since", "quatro", "casa", "baja", "yellow"]

        },
        {

            "Key": "654653643334",

            "Email":"anonymous890@gmail.com",

            "Attributes": ["yellow", "yellow"]

        },
        {

            "Key": "654653643334",

            "Email":"anonymous890@gmail.com",

            "Attributes": ["day", "morning", "sun", "moon","neptunus"]

        }
     ]
    }
   2: Prepare Emails
   Endpoint: https://localhost:44389/api/EmailSender/ Localhost
   Endpoint: https://emailsender20220225010929.azurewebsites.net/api/EmailSender/ Live
   Request : Get
   (For now Prepare email might duplicate email body as you might be calling this api multiple times in a day. One solutions to this can be
   adding a check to avoid this situation and another could be to create a schedular which would be called once in 24 hours.)

