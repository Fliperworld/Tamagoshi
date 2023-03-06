using System;

namespace Tamagoshi
{
    public class MissmatchException : Exception
    {
        public override string Message => "Pokemons names and count missmatch !!!";
    }

    public class ApiResponseException : Exception
    {
        public override string Message => "Api Response Unsuccessful !!!";
    }

    public class TamagoshiFatalException : Exception
    {
        public override string Message => "Tamagoshi Fatal Exception!!!";
    }

}
