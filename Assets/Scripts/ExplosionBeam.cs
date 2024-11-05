using System.Collections;
using UnityEngine;

public class ExplosionBeam : MonoBehaviour
{
    [SerializeField] private float _expansionTime = 0.2f;
    [SerializeField] private float _delay = 0.1f;
    [SerializeField] private float _dispersionTime = 0.2f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        var boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(scale.x, 0, scale.z);

        float t = 0;
        while (t < _expansionTime)
        {
            t += Time.deltaTime * 1.2f;
            if (t >= _expansionTime)
                t = _expansionTime;
            
            transform.localScale = new Vector3(scale.x, Mathf.Lerp(0, scale.y, t / _expansionTime), scale.z);
            yield return new WaitForEndOfFrame();
        }
        
        boxCollider.enabled = true;
        yield return new WaitForSeconds(_delay);

        t = 0;
        while (t < _dispersionTime)
        {
            t += Time.deltaTime * 1.2f;
            if (t >= _dispersionTime)
                t = _dispersionTime;
            
            transform.localScale = new Vector3(scale.x, Mathf.Lerp(scale.y, 0, t / _dispersionTime), scale.z);
            yield return new WaitForEndOfFrame();
        }
        
        Destroy(gameObject);
    }
}
