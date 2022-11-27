namespace AuthenticationAuthorization.Models;

public static class DummyDatabase
{
    public static ICollection<User> Users = new List<User>
    {
        new User(
            email: "victor.florescu@gmail.com",
            password: "Victorflorescu1111",
            new Role("admin")),
        new User(
            email: "florescu.victor@gmail.com",
            password: "Florescuvictor1111",
            new Role("user"))
    };
}