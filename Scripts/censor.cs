///////////////////////////////////////////////////////////////////////////////
// Chat censor strings (MUST BE LOWER CASE!)
///////////////////////////////////////////////////////////////////////////////
//
//NOTE TO TRANSLATORS. These are BAD WORDS which are blanked out in messages when the
//"Censor chat messages" option is enabled in the shell. You don't have
//to translate them literally. Just put whatever words you think proper
//for your language in the list. 
//
//Note that the parser is pretty dumb, and just looks for the sequence of
//characters. For example, we can't censor "ASS" in english because words like
//"assure", "assume", "pass" etc., would be partially blanked out.
//Also if you have multiple word forms of the same obscene word, put the longest 
//first. For example put "cocksucker" before "cock." If you don't do this, the
//parser will partially censor the word on the first pass and miss the rest of it.
//For example, if "cock" is before "cocksucker" in the list, cocksucker would get
//modified to #@$!sucker on the first pass and the rest of it would be missed because
//the partially censored form no longer matches a word in the list.
//

$censor::word0    = "bitch";
$censor::word1    = "cocksucker";
$censor::word2    = "cock";
$censor::word3    = "cunt";
$censor::word4    = "damn";
$censor::word5    = "dick";
$censor::word6    = "douchebag";
$censor::word7    = "faggot";
$censor::word8    = "asshole";
$censor::word9    = "motherfucker";
$censor::word10   = "fuck";
$censor::word11   = "nigger";
$censor::word12   = "piss";
$censor::word13   = "prick";
$censor::word14   = "pussy";
$censor::word15   = "rape";
$censor::word16   = "shit";
$censor::word17   = "vagina";
$censor::word18   = "vulva";
$censor::word19   = "anus";
$censor::word20   = "anal";
$censor::word21   = "crap";
$censor::word22   = "kike";
$censor::word23   = "penis";
$censor::word24   = "poop";
$censor::word25   = "rectum";
$censor::word26   = "semen";
$censor::word27   = "sperm";

