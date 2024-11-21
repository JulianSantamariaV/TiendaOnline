//using TiendaOnline.Data;

//namespace TiendaOnline.Singletons
//{
//    public class DatabaseConnection
//    {
//        private static DatabaseConnection _instance;
//        private static readonly object _lock = new object();
//        private readonly GestionInventarioContext _context;

//        // Constructor privado que recibe las opciones del DbContext
//        private DatabaseConnection(GestionInventarioContext context)
//        {
//            _context = context;
//        }

//        // Método para obtener la instancia única, con el contexto inyectado al principio
//        public static void Initialize(GestionInventarioContext context)
//        {
//            lock (_lock)
//            {
//                if (_instance == null)
//                {
//                    _instance = new DatabaseConnection(context);
//                }
//            }
//        }

//        public static DatabaseConnection Instance
//        {
//            get
//            {
//                if (_instance == null)
//                {
//                    throw new Exception("DatabaseConnection no ha sido inicializado. Llama a Initialize primero.");
//                }
//                return _instance;
//            }
//        }

//        public GestionInventarioContext Context => _context;
//    }
//}
