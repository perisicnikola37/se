using ExpenseTrackerApi.conf;

var builder = BuilderConfig.ConfigureBuilder(args);

var app = builder.Build();
app.ConfigureApp();


