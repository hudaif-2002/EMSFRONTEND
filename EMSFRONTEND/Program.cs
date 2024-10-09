using EMSFRONTEND.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the EmpTaskListService with HttpClient and BaseAddress
builder.Services.AddHttpClient<EmpTaskListService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5293"); // Adjust to your API Base URL
});

// Register the LoginSignupService similarly
builder.Services.AddHttpClient<LoginSignupService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5293"); // Adjust to your backend URL
});

// Add session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Add session middleware

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
