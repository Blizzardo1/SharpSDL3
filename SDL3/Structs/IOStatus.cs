<<<<<<< HEAD
namespace SharpSDL3.Structs;
=======
﻿namespace SharpSDL3.Structs;
>>>>>>> main

public enum IOStatus {
    Ready,     /**< Everything is ready (no errors and not EOF). */
    Error,     /**< Read or write I/O error */
    Eof,       /**< End of file */
    NotReady,  /**< Non blocking I/O, not ready */
    ReadOnly,  /**< Tried to write a read-only buffer */
    WriteOnly  /**< Tried to read a write-only buffer */
<<<<<<< HEAD
}
=======
}
>>>>>>> main
