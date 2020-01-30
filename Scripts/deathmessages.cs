///////////////////////////////////////////////////////////////////////////////
//NOTE TO TRANSLATORS. The following are idiomatic expressions and should not be translated
//literally. Please do your best to create amusing "death messages" that will entertain the 
//people of your homeland.
//
//The death messages insert the handles of the person who died and the handle of the 
//person who killed them automatically in the death message which is displayed.
//
//There are three sections. The first, "generic", is used when a player kills him or
//herself.  This message will be used for all random, accidental deaths (explosion, 
//falling, perhaps others).
//
//The other two sections are used in cases where one player has killed another.
//The second section, "active", places the handle (name) of the KILLER first, then
//the handle (name) of the player who DIED second.
//
//The third section, "passive", places the handle (name) of the player who DIED
//first, then the handle (name) of the KILLER second.
//
//The %s is where the player handles (names) are inserted into the string.
//A player handle string (%s) does not have to be the first word in the message.
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Death descriptions
///////////////////////////////////////////////////////////////////////////////

// generic: player killed accidentally
$deathMessage::genericCount   = 1;
$deathMessage::generic0    = "%s died.";

// active: player kills player
$deathMessage::activeCount    = 18;
$deathMessage::active0     = "%s ventilated %s.";
$deathMessage::active1     = "%s donated %s's body to science.";
$deathMessage::active2     = "%s introduces %s to the agony of defeat.";
$deathMessage::active3     = "%s punched %s's ticket.";
$deathMessage::active4     = "%s demonstrates death without dignity to %s.";
$deathMessage::active5     = "%s invites %s to explore the past tense of being.";
$deathMessage::active6     = "%s offs %s.";
$deathMessage::active7     = "%s gives %s an out of body experience. ";
$deathMessage::active8     = "%s burns %s to smelly jelly.";
$deathMessage::active9     = "%s makes a righteous mess of %s.";
$deathMessage::active10    = "%s composts %s.";
$deathMessage::active11    = "%s invites %s to join the scenery.";
$deathMessage::active12    = "%s gets medieval on %s.";
$deathMessage::active13    = "%s wipes the floor with %s.";
$deathMessage::active14    = "%s parties down on %s.";
$deathMessage::active15    = "%s opens a can of whoopaz on %s.";
$deathMessage::active16    = "%s massacred %s.";
$deathMessage::active17    = "%s inflicts a coup de grace on %s.";

// passive: player killed by player
$deathMessage::passiveCount   = 22;
$deathMessage::passive0    = "%s is chewed up and spit out by %s";
$deathMessage::passive1    = "%s got curb stomped by %s";
$deathMessage::passive2    = "%s gets the Vulcan Nerve Pinch from %s";
$deathMessage::passive3    = "%s felt the burn from %s.";
$deathMessage::passive4    = "%s took it hard and hot from %s";
$deathMessage::passive5    = "%s is now a crispy lawn ornament in %s's yard.";
$deathMessage::passive6    = "%s brought a knife to the gun fight with %s.";
$deathMessage::passive7    = "%s is snuffed by %s.";
$deathMessage::passive8    = "%s gets a flying wedgie from %s.";
$deathMessage::passive9    = "%s gets a nice wet vivisection from %s.";
$deathMessage::passive10   = "%s's ticket was punched by %s.";
$deathMessage::passive11   = "%s's personal space was violated by %s.";
$deathMessage::passive12   = "%s is curb stomped by %s.";
$deathMessage::passive13   = "%s makes an unsuccessful pass at %s.";
$deathMessage::passive14   = "%s gets a little chin music from %s.";
$deathMessage::passive15   = "%s receives a Chicago Overcoat from %s.";
$deathMessage::passive16   = "%s is cut down in the prime of life by %s.";
$deathMessage::passive17   = "%s gets drilled by %s.";
$deathMessage::passive18   = "%s got eighty-sixed by %s.";
$deathMessage::passive19   = "%s plays tackling dummy for %s.";
$deathMessage::passive20   = "%s got slaughtered by %s.";
$deathMessage::passive21   = "%s is annihilated by a lucky shot from %s.";
