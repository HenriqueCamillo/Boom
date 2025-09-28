using System.Collections.Generic;
using UnityEngine;

public class SkillCollectableOrganizer : MonoBehaviour
{
    [SerializeField] SkillExecutor skillExecutor;
    [SerializeField] Transform spawnPosition;
    [SerializeField] float distanceBetweenSpawns;

    [SerializeField] List<SkillCollectable> skillCollectables;

    private void Awake()
    {
        Organize();
    }

    private void Organize()
    {
        bool hasEvenNumberOfSkills = skillCollectables.Count % 2 == 0;
        for (int i = 0; i < skillCollectables.Count; i++)
        {
            SkillCollectable skillCollectable = skillCollectables[i]; 
            if (skillCollectable == null)
                continue;

            float xPosition = spawnPosition.transform.position.x;
            int sign = (i % 2 == 0) ? 1 : -1;
            if (hasEvenNumberOfSkills)
                xPosition += ((distanceBetweenSpawns / 2) + (distanceBetweenSpawns * (i / 2))) * sign;
            else
                xPosition += (distanceBetweenSpawns * ((i + 1) / 2)) * sign;

            skillCollectable.IsStartupCollectable = true;
            skillCollectable.transform.position = new Vector3(xPosition, spawnPosition.transform.position.y, spawnPosition.transform.position.z);
        }
    }

    public void Reset()
    {
        foreach (SkillCollectable skillCollectable in skillCollectables)
            if (skillCollectable != null)
                skillCollectable.gameObject.SetActive(true);
        
        skillExecutor.ClearSkills();
        Organize();
    }
}