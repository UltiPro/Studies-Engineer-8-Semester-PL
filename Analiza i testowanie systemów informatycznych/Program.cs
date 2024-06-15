using SPA;
using SPA.ApplicationModes.Types;

//Init
var app = SPAApp.InitApp(true);

//Tryb testów
app.Run(ApplicationRunModes.Test);

//Normalny tryb działania aplikacji
//app.Run(ApplicationRunModes.Basic, args);
