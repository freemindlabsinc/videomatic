﻿using Ardalis.Specification;

namespace Company.Videomatic.Domain.Specifications;

public class VideoByIdsSpecification : Specification<Video>
{
    public VideoByIdsSpecification(params VideoId[] ids)
    {
        Query.Where(v => ids.Contains(v.Id));
    }
}
