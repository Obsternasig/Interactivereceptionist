This website contain an interactive receptionist / Wayfinding site designed for the Novi8 building
All names are placeholder names in order to avoid having people and companies give us permission to use correct employee numbers, names, and contact info. 

The project is build in Asp Net Core Razor Pages in order to have it crossplatform working in a browser
With the help of bootstrap it has some degree of responsiveness. 
Javascript and Ajax has also been implmented in order to get it interactiveness. example: Notification

SQL: 
In order to get this app to work you will need to connect to the database listed in appsettings.json
* View - Sql Server Object Explorer
* Add Sql Server
* Server Name: {irnovi.database.windows.net} - Authentication {Sql Server Authentication} - Username {Anders} - Password {MAjjDA0218}

Login Admin Panel:
Username: {test@test.com}
Password: {1234-Aa}

Design:
Novi's Designmanuel have been kept in mind during the creation, and with the help of very few classes the color changes there might come in their design manual can be changed.
All pages follow more or less the same design protocol, with a check wether you are logged in or not, and show the desired pages based on that input.

"Known" Bugs or Todos:
- Small Sql table name. Should be called "FindVej" but due to migration issues is still called "Stillingen"
- Not 100% responsiveness across all screen resolutions.
- To comply with known GDPR regulations, it is not allowed to see a complete list of employees. They should be hidden until searched upon, but with their concsent. 
-

Unit Tests:
No Unit Tests have been implemented.






