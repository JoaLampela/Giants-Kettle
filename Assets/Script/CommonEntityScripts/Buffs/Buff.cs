using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Buff : MonoBehaviour
{
    public string _sourceId;
    public string _effectID;
    public int _value;
    EntityEvents events;
    BuffManager buffManager;
    BuffClass buff;

    private void Awake()
    {
        buffManager = GetComponent<BuffManager>();
        events = GetComponent<EntityEvents>();
    }


    private void Start()
    {
        Subscribe();
        buff = new BuffClass(_effectID, _value);
        buffManager.UpdateActiveBuffs(_sourceId, buff);
    }

    private void OnDisable()
    {
        _value = 0;
        Unsubscribe();
        buffManager.RemoveBuff(_sourceId, buff);
        //this event is received by all Buff-scripts that are attached to this gameObject
        //it makes them send updates to BuffManager if the sourceId is the same
        events.UpdateBuff(_sourceId);
    }

    public IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this);
    }


    private void RemoveBuff(string SourceId)
    {
        if(_sourceId == SourceId)
        {
            Destroy(this);
        }
    }

    private void UpdateBuff(string sourceId)
    {
        if(_sourceId == sourceId)
        {
            buffManager.UpdateActiveBuffs(_sourceId, buff);
        }
    }


    private void Subscribe()
    {
        events.OnRemoveBuff += RemoveBuff;
        events.OnUpdateBuff += UpdateBuff;
    }


    private void Unsubscribe()
    {
        events.OnRemoveBuff -= RemoveBuff;
        events.OnUpdateBuff -= UpdateBuff;
    }
}