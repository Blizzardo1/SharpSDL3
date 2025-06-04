using System;

namespace SharpSDL3.Enums;

[Flags]
public enum KeyMod : ushort
{
	KModNone = 0x0000,
	KModLShift = 0x0001,
	KModRShift = 0x0002,
	KModLCtrl = 0x0040,
	KModRCtrl = 0x0080,
	KModLAlt = 0x0100,
	KModRAlt = 0x0200,
	KModLGui = 0x0400,
	KModRGui = 0x0800,
	KModNum = 0x1000,
	KModCaps = 0x2000,
	KModMode = 0x4000,
	KModScroll = 0x8000,
	KModCtrl = KModLCtrl | KModRCtrl,
	KModShift = KModLShift | KModRShift,
	KModAlt = KModRAlt | KModLAlt,
	KModGui = KModRGui | KModLGui
}