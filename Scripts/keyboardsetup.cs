//------------------------------------------------------------------------------
// Description 
//    
// keyboard definition file
//
// allows the the app to bind specific ascii values to keys
// useful for emulating DVORAK and foreign keyboard layouts
// 
// Syntax
//    defineKey( key, lowercase_ascii, uppercase_ascii, name);
//    
//    Hexadecimal ascii values may be used but must be quoted: "0x0d"
//
//    Argument 'name' is used when no ascii value is present when binding actions
//
//
//
//------------------------------------------------------------------------------


// Standard US Keyboard definition

defineKey(KEY_ESCAPE,          "0x1b", "0x1b", "escape");        
defineKey(KEY_1,                  "1",    "!", "");               
defineKey(KEY_2,                  "2",    "@", "");               
defineKey(KEY_3,                  "3",    "#", "");               
defineKey(KEY_4,                  "4",    "$", "");               
defineKey(KEY_5,                  "5",    "%", "");               
defineKey(KEY_6,                  "6",    "^", "");               
defineKey(KEY_7,                  "7",    "&", "");               
defineKey(KEY_8,                  "8",    "*", "");               
defineKey(KEY_9,                  "9",    "(", "");               
defineKey(KEY_0,                  "0",    ")", "");               
defineKey(KEY_MINUS,              "-",    "_", "");               
defineKey(KEY_EQUALS,             "=",    "+", "");               
defineKey(KEY_BACK,            "0x08", "0x08", "backspace");     
defineKey(KEY_TAB,             "0x09", "0x09", "tab");           
defineKey(KEY_Q,                  "q",    "Q", "");               
defineKey(KEY_W,                  "w",    "W", "");               
defineKey(KEY_E,                  "e",    "E", "");               
defineKey(KEY_R,                  "r",    "R", "");               
defineKey(KEY_T,                  "t",    "T", "");               
defineKey(KEY_Y,                  "y",    "Y", "");               
defineKey(KEY_U,                  "u",    "U", "");               
defineKey(KEY_I,                  "i",    "I", "");               
defineKey(KEY_O,                  "o",    "O", "");               
defineKey(KEY_P,                  "p",    "P", "");               
defineKey(KEY_LBRACKET,           "[",    "{", "");               
defineKey(KEY_RBRACKET,           "]",    "}", "");               
defineKey(KEY_RETURN,          "0x0d", "0x0d", "enter");         
defineKey(KEY_LCONTROL,            "",     "", "lcontrol");      
defineKey(KEY_A,                  "a",    "A", "");               
defineKey(KEY_S,                  "s",    "S", "");               
defineKey(KEY_D,                  "d",    "D", "");               
defineKey(KEY_F,                  "f",    "F", "");               
defineKey(KEY_G,                  "g",    "G", "");               
defineKey(KEY_H,                  "h",    "H", "");               
defineKey(KEY_J,                  "j",    "J", "");               
defineKey(KEY_K,                  "k",    "K", "");               
defineKey(KEY_L,                  "l",    "L", "");               
defineKey(KEY_SEMICOLON,          ";",    ":", "");               
defineKey(KEY_APOSTROPHE,         "'", "0x22", "quote");         
defineKey(KEY_GRAVE,              "`",    "~", "");               
defineKey(KEY_LSHIFT,              "",     "", "lshift");        
defineKey(KEY_BACKSLASH,         "\\",    "|", "");               
defineKey(KEY_Z,                  "z",    "Z", "");               
defineKey(KEY_X,                  "x",    "X", "");               
defineKey(KEY_C,                  "c",    "C", "");               
defineKey(KEY_V,                  "v",    "V", "");               
defineKey(KEY_B,                  "b",    "B", "");               
defineKey(KEY_N,                  "n",    "N", "");               
defineKey(KEY_M,                  "m",    "M", "");               
defineKey(KEY_COMMA,              ",",    "<", "comma");
defineKey(KEY_PERIOD,             ".",    ">", "period");        
defineKey(KEY_SLASH,              "/",    "?", "");               
defineKey(KEY_RSHIFT,              "",     "", "rshift");        
defineKey(KEY_MULTIPLY,           "*",    "*", "numpad*");       
defineKey(KEY_LMENU,               "",     "", "lalt");          
defineKey(KEY_SPACE,              " ",    " ", "space");         
defineKey(KEY_CAPITAL,             "",     "", "capslock");      
defineKey(KEY_F1,                  "",     "", "f1");            
defineKey(KEY_F2,                  "",     "", "f2");            
defineKey(KEY_F3,                  "",     "", "f3");            
defineKey(KEY_F4,                  "",     "", "f4");            
defineKey(KEY_F5,                  "",     "", "f5");            
defineKey(KEY_F6,                  "",     "", "f6");            
defineKey(KEY_F7,                  "",     "", "f7");            
defineKey(KEY_F8,                  "",     "", "f8");            
defineKey(KEY_F9,                  "",     "", "f9");            
defineKey(KEY_F10,                 "",     "", "f10");           
defineKey(KEY_NUMLOCK,             "",     "", "numlock");       
defineKey(KEY_SCROLL,              "",     "", "scroll");        
defineKey(KEY_NUMPAD7,            "7",    "7", "numpad7");       
defineKey(KEY_NUMPAD8,            "8",    "8", "numpad8");       
defineKey(KEY_NUMPAD9,            "9",    "9", "numpad9");       
defineKey(KEY_SUBTRACT,           "-",    "-", "numpad-");       
defineKey(KEY_NUMPAD4,            "4",    "4", "numpad4");       
defineKey(KEY_NUMPAD5,            "5",    "5", "numpad5");       
defineKey(KEY_NUMPAD6,            "6",    "6", "numpad6");       
defineKey(KEY_ADD,                "+",    "+", "numpad+");       
defineKey(KEY_NUMPAD1,            "1",    "1", "numpad1");       
defineKey(KEY_NUMPAD2,            "2",    "2", "numpad2");       
defineKey(KEY_NUMPAD3,            "3",    "3", "numpad3");       
defineKey(KEY_NUMPAD0,            "0",    "0", "numpad0");       
defineKey(KEY_DECIMAL,            ".",    ".", "");               
defineKey(KEY_F11,                 "",     "", "f11");           
defineKey(KEY_F12,                 "",     "", "f12");           
defineKey(KEY_F13,                 "",     "", "f13");           
defineKey(KEY_F14,                 "",     "", "f14");           
defineKey(KEY_F15,                 "",     "", "f15");           
defineKey(KEY_KANA,                "",     "", "");               
defineKey(KEY_CONVERT,             "",     "", "");               
defineKey(KEY_NOCONVERT,           "",     "", "");               
defineKey(KEY_YEN,                 "",     "", "");               
defineKey(KEY_NUMPADEQUALS,       "=",    "=", "numpadequals");  
defineKey(KEY_CIRCUMFLEX,          "",     "", "");               
defineKey(KEY_AT,                 "@",    "@", "");               
defineKey(KEY_COLON,              ":",    ":", "");               
defineKey(KEY_UNDERLINE,          "_",    "_", "");               
defineKey(KEY_KANJI,               "",     "", "");               
defineKey(KEY_STOP,                "",     "", "stop");          
defineKey(KEY_AX,                  "",     "", "");               
defineKey(KEY_UNLABELED,           "",     "", "");               
defineKey(KEY_NUMPADENTER,     "0x0d", "0x0d", "numpadenter");   
defineKey(KEY_RCONTROL,            "",     "", "rcontrol");      
defineKey(KEY_NUMPADCOMMA,        ",",    ",", "numpadcomma");   
defineKey(KEY_DIVIDE,             "/",    "/", "numpad/");       
defineKey(KEY_SYSRQ,               "",     "", "sysreq");        
defineKey(KEY_RMENU,               "",     "", "ralt");          
defineKey(KEY_HOME,                "",     "", "home");          
defineKey(KEY_UP,                  "",     "", "up");            
defineKey(KEY_PRIOR,               "",     "", "prior");         
defineKey(KEY_LEFT,                "",     "", "left");          
defineKey(KEY_RIGHT,               "",     "", "right");         
defineKey(KEY_END,                 "",     "", "end");           
defineKey(KEY_DOWN,                "",     "", "down");          
defineKey(KEY_NEXT,                "",     "", "next");          
defineKey(KEY_INSERT,              "",     "", "insert");        
defineKey(KEY_DELETE,              "",     "", "delete");        
defineKey(KEY_LWIN,                "",     "", "win");           
defineKey(KEY_RWIN,                "",     "", "win");           
defineKey(KEY_APPS,                "",     "", "app");           
