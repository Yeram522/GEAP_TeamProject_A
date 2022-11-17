using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//필드에 동적으로 포인트를 생성하고 관리.
public class ElemRockRolling : MonoBehaviour
{
    public GameObject SpawnPrefab; //스폰할 Rock Prefab
    public GameObject totalStartPoints;
    private List<GameObject> m_startPoints;

    private bool trigger = true;
    public void Initialzie()
    {
        m_startPoints = new List<GameObject>();
        for (int i = 0; i < totalStartPoints.transform.childCount; i++)
        {
            m_startPoints.Add(totalStartPoints.transform.GetChild(i).gameObject);
        }

        if (m_startPoints.Count <= 0) return;

        int count = Random.Range(1, m_startPoints.Count);
        count = 3;

        StartCoroutine(CreateRollingRock(count));
    }


    IEnumerator CreateRollingRock(int count)
    {
        Vector3 dir = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;

        for (int i = 0; i <count; i ++)
        {
            GameObject rock = Instantiate(SpawnPrefab, m_startPoints[i].transform.position, SpawnPrefab.transform.rotation);
            StartCoroutine(Rolling(rock, dir));
        }

        yield return null;
    }

    IEnumerator Rolling(GameObject _rock, Vector3 _dir)
    {
        Destroy(_rock, 10.0f);
        //돌 굴러가는 코드 endp[idx]를 향해서!\
        _rock.GetComponent<Rigidbody>().AddForce(_dir);
        while (trigger)
        {
            _rock.GetComponent<Rigidbody>().AddForce(_dir);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }

 
}
