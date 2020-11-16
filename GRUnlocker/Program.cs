namespace GRUnlocker {
    class Program {

        public static string[] ARGS;
        
        static void Main(string[] args) {
            ARGS = args;
            // load config if any
            Config.getInstance().Load();

            // handle input
            InputHandler handler = new InputHandler();
            handler.Handle();
        }
    }
}