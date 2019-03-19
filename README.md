## TrelloAPI

TrelloAPI is a booking event API inspired by an advanced CSS tutorial on udemy. Here, admin users can create, view, delete, update, 
bookings. A booking is an event which can be ordered for by people. I limited the scope of the project to Flights, Hotels, Tours, and Car Rentals.
The application can be extended to include other booking events as the backend(ASPNETCORE) web services has a generic class for booking 
events.

Users can view bookings, make comments about bookings, view other people's comments about that booking, book for events etc. 
Users can also upload their photos and chat with other registered users. 

The Application uses JWT token authentication to ensure that only logged in users have access to admin and user roles.
The pictures of users are stored using cloudinary's API.

The Application has methods to seed random users alongside dummy comments, bookings, features, etc.

The application's front end was written in ANGULAR 6 while the Web API was written with ASPNETCORE 2.1
