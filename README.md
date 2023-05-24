# ToDoApp
Unit tests for Razor pages models are not fully implemented. 
Because I have only limited experience with frontend in MVC and no experience with Razor pages, it took me more time to write the client part 
and thus I had not much time left for writing all the unit tests. 
For Index.cshtml.cs I refactored the code and created HttpClientWrapper because HttpClient cannot be directly mocked. 
I wrote sample unit test for testing OnPostAsync method in Index.cshtml.cs.
