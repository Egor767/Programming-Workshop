using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;

namespace PlaywrightTests;

public static class RequestsConstansts{
    public static string baseUrl = "https://restful-booker.herokuapp.com";
    public static string username = "admin";
    public static string password = "password123";
    public static int bookingID = -1;
    public static Dictionary<string, object> booking = new(){
        {"firstname", "Egor"},
        {"lastname", "Mumiyak"},
        {"totalprice", 1000},
        {"depositpaid", true},
        {"bookingdates", new Dictionary<string, string>(){
            {"checkin", "2024-01-01"},
            {"checkout", "2024-01-02"}
        }},
        {"additionalneeds", "Water"}
    };
    public static Dictionary<string, object> updBooking = new(){
        {"firstname", "Not Egor"},
        {"lastname", "Not Mumiyak"},
        {"totalprice", 2000},
        {"depositpaid", false},
        {"bookingdates", new Dictionary<string, string>(){
            {"checkin", "2024-01-01"},
            {"checkout", "2024-01-02"}
        }},
        {"additionalneeds", "Water"}
    };
    
}

