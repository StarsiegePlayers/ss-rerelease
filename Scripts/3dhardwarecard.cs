$Hardware::3D::Type1 = "3Dfx Chipset";
$Hardware::3D::Callback1 = "OptionsVideo::setUpGlide();";
$Hardware::3D::Type2 = "nVidia Riva TNT Chipset";
$Hardware::3D::Callback2 = "OptionsVideo::setUpTNT();";
$Hardware::3D::Type3 = "nVidia Riva TNT2 Chipset";
$Hardware::3D::Callback3 = "OptionsVideo::setUpTNT2();";
$Hardware::3D::Type4 = "i740 Chipset";
$Hardware::3D::Callback4 = "OptionsVideo::setUpI740();";
$Hardware::3D::Type5 = "S3 Savage 3D";
$Hardware::3D::Callback5 = "OptionsVideo::setUpSavage3D();";
//$Hardware::3D::Type6 = "ATI Rage 128 Chipset";
//$Hardware::3D::Callback6 = "OptionsVideo::setUpRage128();";
//$Hardware::3D::Type7 = "Matrox G200 Chipset";
//$Hardware::3D::Callback7 = "OptionsVideo::setUpG200();";
//$Hardware::3D::Type8 = "Matrox G400 Chipset";
//$Hardware::3D::Callback8 = "OptionsVideo::setUpG400();";


function OptionsVideo::setUpGenericOpenGL()
{
   echo("setting up generic OpenGL");

   $pref::OpenGL::NoPackedTextures     = true;
   $pref::OpenGL::NoPalettedTextures   = true;
   $pref::OpenGL::VisDistCap           = 750;

   flushTextureCache();
}

function OptionsVideo::setUpGlide()
{
   echo("setting up Glide");

   flushTextureCache();
}

function OptionsVideo::setUpTNT()
{
   echo("setting up Riva TNT");

   $pref::OpenGL::NoPackedTextures     = false;
   $pref::OpenGL::NoPalettedTex        = true;
   $pref::OpenGL::VisDistCap           = 1600;

   flushTextureCache();
}

function OptionsVideo::setUpTNT2()
{
   echo("setting up Riva TNT2");

   $pref::OpenGL::NoPackedTextures     = false;
   $pref::OpenGL::NoPalettedTex        = true;
   $pref::OpenGL::VisDistCap           = 1800;

   flushTextureCache();
}

function OptionsVideo::setUpI740()
{
   echo("setting up i740");

   $pref::OpenGL::NoPackedTextures     = true;
   $pref::OpenGL::NoPalettedTextures   = true;
   $pref::OpenGL::VisDistCap           = 750;

   flushTextureCache();
}

function OptionsVideo::setUpSavage3D()
{
   echo("setting up S3 Savage 3D");

   $pref::OpenGL::NoPackedTextures     = true;
   $pref::OpenGL::NoPalettedTextures   = true;
   $pref::OpenGL::VisDistCap           = 750;

   flushTextureCache();
}

function OptionsVideo::setUpRage128()
{
   echo("setting up ATI Rage 128");

   $pref::OpenGL::NoPackedTextures     = true;
   $pref::OpenGL::NoPalettedTextures   = true;
   $pref::OpenGL::VisDistCap           = 1500;

   flushTextureCache();
}

function OptionsVideo::setUpG200()
{
   echo("setting up Matrox G200");

   $pref::OpenGL::NoPackedTextures     = true;
   $pref::OpenGL::NoPalettedTextures   = true;
   $pref::OpenGL::VisDistCap           = 1000;

   flushTextureCache();
}

function OptionsVideo::setUpG400()
{
   echo("setting up Matrox G400");

   $pref::OpenGL::NoPackedTextures     = true;
   $pref::OpenGL::NoPalettedTextures   = true;
   $pref::OpenGL::VisDistCap           = 1200;

   flushTextureCache();
}

