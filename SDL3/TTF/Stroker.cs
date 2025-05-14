using System.Runtime.InteropServices;

namespace SharpSDL3.TTF;

[StructLayout(LayoutKind.Sequential)]
public struct Stroker {
    public long AngleIn;             /* direction into curr join */
    public long AngleOut;            /* direction out of join  */
    public LongVector Center;               /* current position */
    public long LineLength;          /* length of last lineto */
    public char FirstPoint;          /* is this the start? */
    public char SubpathOpen;         /* is the subpath open? */
    public long SubpathAngle;        /* subpath start direction */
    public LongVector SubpathStart;        /* subpath start position */
    public long SubpathLineLength;  /* subpath start lineto len */
    public char HandleWideStrokes;  /* use wide strokes logic? */

    public LineCap LineCap;
    public LineJoin LineJoin;
    public LineJoin LineJoinSaved;
    public long MiterLimit;
    public long Radius;

    public StrokeBorderRec[] Borders; // [2]
    public FtLibrary Library;

}
