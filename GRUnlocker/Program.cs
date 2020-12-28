namespace GRUnlocker {
    class Program {

        static void Main(string[] args) {
            System.Console.Title = "Ghostrunner Unlocker";
            // load config if any
            Config.getInstance().Load(args);

            // handle input
            InputHandler handler = new InputHandler();
            handler.Handle();
        }
    }
}