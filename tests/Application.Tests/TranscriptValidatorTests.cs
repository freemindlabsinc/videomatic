﻿namespace Application.Tests;

public class TranscriptValidatorTests
{
    public ValidatorHelper ValidatorHelper { get; }

    public TranscriptValidatorTests(IServiceProvider serviceProvider)
    {
        ValidatorHelper = new ValidatorHelper(serviceProvider);
    }
}
