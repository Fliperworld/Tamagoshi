using System;

namespace Tamagoshi
{
    internal class MissmatchException : Exception
    {
        public override string Message => "Pokemons names and count missmatch !!!";
    }

    internal class ApiResponseException : Exception
    {
        public override string Message => "Api Response Unsuccessful !!!";
    }

}
