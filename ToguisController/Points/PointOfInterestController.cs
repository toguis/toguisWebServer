using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToguisModel;

namespace ToguisController.Points
{
    public class PointOfInterestController
    {
        public List<TG_INTEREST_POINT> GetPoints(String login,
                                                 int cityId, 
                                                 bool getMonument, 
                                                 bool getMuseum, 
                                                 bool getHotel, 
                                                 bool getRestaurant,
                                                 bool getInterest,                                       
                                                 bool getBuilding, 
                                                 bool getTransport, 
                                                 bool getEvent,
                                                 int language
                                          )
        {
            List<TG_INTEREST_POINT> loResult =  null;
            using (ToguisEntities loContext = new ToguisEntities())
            {
                try
                {
                    loContext.Configuration.ProxyCreationEnabled = false;
                    List<int> loPoiId = new List<int>();

                    if (getMonument) loPoiId.Add(1);
                    if (getMuseum) loPoiId.Add(2);
                    if (getHotel) loPoiId.Add(3);
                    if (getRestaurant) loPoiId.Add(4);
                    if (getInterest) loPoiId.Add(5);
                    if (getBuilding) loPoiId.Add(6);
                    if (getTransport) loPoiId.Add(7);
                    if (getEvent) loPoiId.Add(8);
                 
                    loResult = (from item in loContext.TG_INTEREST_POINT
                                    where item.CITY_ID == cityId && 
                                          loPoiId.Contains(item.PTYP_ID)
                                    select item).ToList<TG_INTEREST_POINT>();
 
                    foreach (var item in loResult)
                    {
                        loContext.Entry(item).State = EntityState.Detached;
                        List<TG_POI_USER_DATA> loUserData = loContext.TG_POI_USER_DATA.Where(p => p.USR_ID.Equals(login)).ToList<TG_POI_USER_DATA>();
                        item.TG_POI_USER_DATA = loUserData;

                        List<TG_POI_DESCRIPTION> loDescription = loContext.TG_POI_DESCRIPTION.Where(p => p.LNG_ID == language).ToList<TG_POI_DESCRIPTION>();
                        item.TG_POI_DESCRIPTION = loDescription;
                    }
                                     
                }
                catch (Exception ex)
                {
                }
            }
            return loResult;            
        }

        public TG_INTEREST_POINT GetPoint(String login, int poiId, int language)
        {
            TG_INTEREST_POINT loResult = null;
            using (ToguisEntities loContext = new ToguisEntities())
            {
                try
                {
                    loContext.Configuration.ProxyCreationEnabled = false;
                    loResult = (from item in loContext.TG_INTEREST_POINT
                                where item.POI_ID == poiId 
                                select item).FirstOrDefault();
                    
                    loContext.Entry(loResult).State = EntityState.Detached;
                    try
                    {
                        var loRating = (from item in loContext.TG_POI_USER_DATA
                                        where item.POI_ID == poiId  &&
                                              item.UDAT_RATING != null
                                        group item by item.POI_ID into d
                                        select new { SumItems = d.Sum(p => p.UDAT_RATING) / d.Count() }).FirstOrDefault();

                        loResult.RATING = (float)loRating.SumItems;
                    }
                    catch (Exception)
                    {
                    }


                    List<TG_POI_USER_DATA> loUserData = loContext.TG_POI_USER_DATA.Where(p => p.USR_ID.Equals(login)).ToList<TG_POI_USER_DATA>();
                    loResult.TG_POI_USER_DATA = loUserData;

                    List<TG_POI_DESCRIPTION> loDescription = loContext.TG_POI_DESCRIPTION.Where(p => p.LNG_ID == language).ToList<TG_POI_DESCRIPTION>();
                    loResult.TG_POI_DESCRIPTION = loDescription;
                }
                catch (Exception ex)
                {
                }
            }
            return loResult;            

        }

        public List<TG_COMMENTS> GetComments(int poiId)
        {
            List<TG_COMMENTS> loResult = new List<TG_COMMENTS>();
            using (ToguisEntities loContext = new ToguisEntities())
            {
                try
                {
                    loResult = (from item in loContext.TG_COMMENTS
                                where item.POI_ID == poiId
                                select item
                               ).ToList();
                }
                catch (Exception ex) 
                {

                }
            }
            return loResult;
        }

        public int SetComment(TG_COMMENTS comment)
        {

            using (ToguisEntities loContext = new ToguisEntities())
            {
                try
                {
                    loContext.TG_COMMENTS.Add(comment);
                    loContext.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    return 1;
                }
                catch (Exception ex)
                {
                    return 2;
                }
            }
            return 0;
        }

        public int SetRating(String login, int poiId, float rating)
        {
            using (ToguisEntities loContext = new ToguisEntities())
            {
                try
                {
                    TG_POI_USER_DATA loUserData = loContext.TG_POI_USER_DATA.Where(p => p.POI_ID == poiId && p.USR_ID.Equals(login)).FirstOrDefault();
                    if (loUserData != null)
                    {
                        loUserData.UDAT_RATING = rating;
                    }
                    else
                    {
                        loUserData = new TG_POI_USER_DATA();
                        loUserData.POI_ID = poiId;
                        loUserData.USR_ID = login;
                        loUserData.UDAT_FAVORITE = false;
                        loUserData.UDAT_VISITED = false;
                        loUserData.UDAT_RATING = rating;
                        loContext.TG_POI_USER_DATA.Add(loUserData);
                    }
                    
                    loContext.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    return 1;
                }
                catch (Exception ex)
                {
                    return 2;
                }
            }
            return 0;      
        }

        public float GetRating(String login, int poiId)
        {
            float lfResult = -1f;
            using (ToguisEntities loContext = new ToguisEntities())
            {
                try
                {
                    TG_POI_USER_DATA loUserData = loContext.TG_POI_USER_DATA.Where(p => p.POI_ID == poiId && p.USR_ID.Equals(login)).FirstOrDefault();
                    lfResult = (float)loUserData.UDAT_RATING;
                }
                catch (Exception ex)
                {
                }
            }
            return lfResult;   
        }

        public int SetFavorite(String login, int poiId, bool value)
        {
            using (ToguisEntities loContext = new ToguisEntities())
            {
                try
                {
                    TG_POI_USER_DATA loUserData = loContext.TG_POI_USER_DATA.Where(p => p.POI_ID == poiId && p.USR_ID.Equals(login)).FirstOrDefault();
                    if (loUserData != null)
                    {
                        loUserData.UDAT_FAVORITE = value;
                    }
                    else
                    {
                        loUserData = new TG_POI_USER_DATA();
                        loUserData.POI_ID = poiId;
                        loUserData.USR_ID = login;
                        loUserData.UDAT_FAVORITE = value;
                        loUserData.UDAT_VISITED = false;
                        loUserData.UDAT_RATING = null;
                        loContext.TG_POI_USER_DATA.Add(loUserData);
                    }

                    loContext.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    return 1;
                }
                catch (Exception ex)
                {
                    return 2;
                }
            }
            return 0;              
        }

        public int SetVisited(String login, int poiId, bool value)
        {
            using (ToguisEntities loContext = new ToguisEntities())
            {
                try
                {
                    TG_POI_USER_DATA loUserData = loContext.TG_POI_USER_DATA.Where(p => p.POI_ID == poiId && p.USR_ID.Equals(login)).FirstOrDefault();
                    if (loUserData != null)
                    {
                        loUserData.UDAT_VISITED = value;
                    }
                    else
                    {
                        loUserData = new TG_POI_USER_DATA();
                        loUserData.POI_ID = poiId;
                        loUserData.USR_ID = login;
                        loUserData.UDAT_FAVORITE = false;
                        loUserData.UDAT_VISITED = value;
                        loUserData.UDAT_RATING = null;
                        loContext.TG_POI_USER_DATA.Add(loUserData);
                    }

                    loContext.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    return 1;
                }
                catch (Exception ex)
                {
                    return 2;
                }
            }
            return 0;
        }

    }
}
