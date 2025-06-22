void LoopFunction(float input, int steps, out float result)
{
    result = input;
    for (int i = 0; i < steps; i++)
    {
        if (result > 0.5) {
            result *= 0.8; // example conditional logic
        }
        else {
            result += 0.1;
        }
    }
}
