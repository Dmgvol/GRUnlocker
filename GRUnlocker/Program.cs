namespace GRUnlocker {
    class Program {

        public static string[] ARGS;
        static void Main(string[] args) {
            ARGS = args;
            InputHandler handler = new InputHandler();
            handler.Handle();   
        }
    }
}